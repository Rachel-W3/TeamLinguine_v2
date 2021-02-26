using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefScript : VehicleScript
{
    // Variables for FOV
    public float fovAngle = 110f;
    public bool playerInSight;
    private GameObject player;

    // Variables for wandering
    private GameObject waypoint;
    public PathScript path;

    // Variables for obstacle avoidance
    private ObstacleManager obstacleManager;
    public Rigidbody rb;

    //Animation Variable
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        path = GameObject.Find("Path").GetComponent<PathScript>();
        waypoint = path.waypoints[0];
        vehiclePosition = waypoint.transform.position;
        animator = GetComponentInChildren<Animator>();
        //animator.SetTrigger("animation_1");
        obstacleManager = GetComponent<ObstacleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (obstacleManager != null)
        {
            foreach (GameObject obstacle in obstacleManager.obstacles)
            {
                ApplyForce(AvoidObstacles(obstacle) * 10.0f); // scaled up
            }
        }

        animator.SetTrigger("animation_8");
        animator.Play("animation_8");

        vehiclePosition = transform.position;
        // Adjusts vehicle height based on terrain height
        vehiclePosition.y = Terrain.activeTerrain.SampleHeight(vehiclePosition);

        rb.AddForce(vehiclePosition.x, vehiclePosition.y, vehiclePosition.z, ForceMode.Acceleration);
    }

    public override void CalcSteeringForces()
    {
        Vector3 totalForce = new Vector3();

        if(playerInSight)
        {
            totalForce += Seek(player);
        }
        else
        {
            totalForce += ToNextWP();
        }

        // Scales totalForce to maxSpeed
        totalForce.Normalize();
        totalForce = totalForce * maxSpeed;

        ApplyForce(totalForce);
    }

    public Vector3 ToNextWP()
    {
        Vector3 vToWP = waypoint.transform.position - transform.position;
        float minDist = waypoint.GetComponent<Waypoint>().radius + avoidRadius;

        if (vToWP.magnitude < minDist)
        {
            waypoint = path.NextWP();
        }

        return Seek(waypoint);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < fovAngle * 0.5f)
            {
                SphereCollider col = GetComponent<SphereCollider>();
                if(Physics.Raycast(transform.position, direction.normalized, col.radius))
                {
                    playerInSight = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInSight = false;
    }
}
