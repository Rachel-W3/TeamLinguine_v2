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
    public GameObject waypoint;
    public PathScript path;

    //Animation Variable
    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        path = GameObject.Find("Path").GetComponent<PathScript>();
        waypoint = path.waypoints[0];
        vehiclePosition = waypoint.transform.position;
        //animator = GetComponentInChildren<Animator>();

        //animator.SetTrigger("animation_1");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        //animator.SetTrigger("animation_2");
        //animator.Play("animation_2");

        vehiclePosition = transform.position;
        // Adjusts vehicle height based on terrain height
        vehiclePosition.y = Terrain.activeTerrain.SampleHeight(vehiclePosition);

        transform.position = vehiclePosition;
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
            // Rat is in the trigger, but we still need to check
            // if he's in the cone of vision, so this defaults to false
            playerInSight = false;

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
}
