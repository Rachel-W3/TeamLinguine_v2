using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleScript : MonoBehaviour
{
    // Vectors necessary for force-based movement
    public Vector3 vehiclePosition;
    public Vector3 acceleration;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 futurePos;

    // Floats
    public float mass;
    public float maxSpeed;
    public float maxForce;
    public float avoidRadius = 3f; // radius for obstacle avoidance and separation

    //// To prevent objects from leaving the floor
    //public float topBound;
    //public float bottomBound;
    //public float leftBound;
    //public float rightBound;

    //protected SceneManager sceneManager;

    protected void Start()
    {
        //sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        vehiclePosition = transform.position;
        maxForce = 1f;
        //radius = 0.8f;
        //SetBounds();
    }

    // Update is called once per frame
    protected void Update()
    {
        futurePos = transform.position + (velocity.normalized);
        //ApplyForce(KeepInBounds());
        //ApplyForce(Separation());
        CalcSteeringForces();

        transform.forward = direction;
        acceleration.y = 0;

        velocity += acceleration * Time.deltaTime;
        vehiclePosition += velocity * Time.deltaTime;
        direction = velocity.normalized;
        acceleration = Vector3.zero;
        transform.position = vehiclePosition;
    }

    public abstract void CalcSteeringForces();

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public Vector3 Seek(Vector3 targetPosition)
    {
        // Step 1: Find DV (desired velocity)
        // TargetPos - CurrentPos
        Vector3 desiredVelocity = targetPosition - vehiclePosition;

        float d = desiredVelocity.magnitude;
        desiredVelocity.Normalize();
        
        // If desiredVelocity is small enough (11 is an arbitrary number that looks like it works the best)
        if (d < 11.0f)
        {
            // set magnitude according to how close we are
            float m = map(d, 0.0f, 11.0f, 0.0f, maxSpeed);
            desiredVelocity = desiredVelocity * m;
        }
        // otherwise, go at full speed
        else
        {
            desiredVelocity = desiredVelocity * maxSpeed;
        }
       
        // Step 3:  Calculate seeking steering force
        Vector3 seekingForce = desiredVelocity - velocity;
        //seekingForce.Normalize();
        //seekingForce = seekingForce * maxForce;

        // Step 4: Return force
        return seekingForce;
    }

    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    public Vector3 Pursue(GameObject target)
    {
        return Seek(target.GetComponent<VehicleScript>().futurePos);
    }

    /// map function equivalent from Processing - Re-maps a number from one range to another.
    float map(float value, float a1, float a2, float b1, float b2)
    {
        return b1 + (value - a1) * (b2 - b1) / (a2 - a1);
    }

    public Vector3 AvoidObstacles(GameObject obstacle)
    {
        Vector3 vectorToObstacle = obstacle.transform.position - transform.position;
        float minDistance = avoidRadius;

        // Is obstacle behind?
        if (Vector3.Dot(transform.forward, vectorToObstacle) < 0)
        {
            return Vector3.zero;
        }
        // Far enough ahead?
        if (vectorToObstacle.sqrMagnitude > minDistance * minDistance)
        {
            return Vector3.zero;
        }
        // Far enough to the right/left?
        if (Mathf.Abs(Vector3.Dot(transform.right, vectorToObstacle)) >= minDistance)
        {
            return Vector3.zero;
        }

        // If all fails...
        // Is obstacle on the right?
        if (Vector3.Dot(transform.right, vectorToObstacle) > 0)
        {
            return -(transform.right * maxSpeed);
        }
        else
        {
            return transform.right * maxSpeed;
        }
    }

    #region Other steering behaviors we may need
    //public Vector3 Flee(Vector3 targetPos)
    //{
    //    // Step 1: Find DV (desired velocity)
    //    Vector3 desiredVelocity = transform.position - targetPos;

    //    // Step 2: Scale vel to max speed
    //    // desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
    //    desiredVelocity.Normalize();
    //    desiredVelocity = desiredVelocity * maxSpeed;

    //    // Step 3:  Calculate fleeing steering force
    //    Vector3 fleeingForce = desiredVelocity - velocity;

    //    // Step 4: Return force
    //    return fleeingForce;
    //}

    //public Vector3 Flee(GameObject target)
    //{
    //    return Flee(target.transform.position);
    //}

    //public Vector3 Evade(GameObject target)
    //{
    //    return Flee(target.GetComponent<VehicleScript>().futurePos);
    //}


    //public Vector3 Separation()
    //{
    //    List<GameObject> tooClose = GetNearby(gameObject.tag, avoidRadius);

    //    Vector3 steerTotal = new Vector3();
    //    if(tooClose.Count == 0)
    //    {
    //        return steerTotal;
    //    }

    //    foreach(GameObject neighbor in tooClose)
    //    {
    //        Vector3 steerVector = transform.position - neighbor.transform.position;
    //        float dist = steerVector.magnitude;
    //        steerVector = steerVector.normalized / dist;
    //        steerTotal += steerVector * 10f; // scaled up for visible effect
    //    }

    //    return steerTotal;
    //}

    //public Vector3 KeepInBounds()
    //{
    //    if (vehiclePosition.x < leftBound + 2f || vehiclePosition.x > rightBound - 2f ||
    //        vehiclePosition.z < bottomBound + 2f || vehiclePosition.z > topBound - 2f )
    //    {
    //        return Seek(new Vector3(0, transform.position.y, 0)) * 3f;
    //    }
    //    else
    //    {
    //        return Vector3.zero;
    //    }
    //}

    // Helper for KeepInBounds method
    //void SetBounds()
    //{
    //    float floorLength = sceneManager.floor.transform.localScale.z;
    //    float floorWidth = sceneManager.floor.transform.localScale.x;

    //    topBound = (floorLength / 2);
    //    bottomBound = -topBound;
    //    rightBound = (floorWidth / 2);
    //    leftBound = -rightBound;
    //}

    //public List<GameObject> GetNearby(string tag, float area)
    //{
    //List<GameObject> members = new List<GameObject>();

    //if (tag == "Human")
    //{
    //    // If this object is a human, be aware of other humans
    //    members.AddRange(sceneManager.activeHumans);
    //}
    //else if (tag == "Zombie")
    //{
    //    // If this object is a zombies, be aware of other zombies
    //    members.AddRange(sceneManager.activeZombies);
    //    members.Remove(gameObject);
    //}

    //// This game object shouldn't have to worry about
    //// avoiding itself
    //members.Remove(gameObject);

    //List<GameObject> tooClose = new List<GameObject>();

    //foreach(GameObject member in members)
    //{
    //    Vector3 vToNeighbor = member.transform.position - transform.position;
    //    if (area >= vToNeighbor.magnitude)
    //    {
    //        tooClose.Add(member);
    //    }
    //}

    //return tooClose;
    //}
    #endregion

    public GameObject GetNearestObject(List<GameObject> objList)
    {
        GameObject nearestObject = null;
        float minDistSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        if (objList.Count > 0)
        {
            foreach (GameObject potentialNeighbor in objList)
            {
                Vector3 directionToTarget = potentialNeighbor.transform.position - currentPos;
                float distanceSqrToTarget = directionToTarget.sqrMagnitude;
                if (distanceSqrToTarget < minDistSqr)
                {
                    minDistSqr = distanceSqrToTarget;
                    nearestObject = potentialNeighbor;
                }
            }
        }

        return nearestObject;
    }

    /// <summary>
    /// Wanders around randomly -- NOT for pathfinders
    /// </summary>
    /// <returns></returns>
    public Vector3 Wander()
    {
        Vector3 circlePos = transform.position + transform.forward * 2f;
        float radius = 2f;
        float angle = Random.Range(0, 180);

        Vector3 wanderTo = new Vector3();
        wanderTo.x = circlePos.x + Mathf.Cos(angle) * radius;
        wanderTo.z = circlePos.z + Mathf.Sin(angle) * radius;

        return Seek(wanderTo);
    }
}
