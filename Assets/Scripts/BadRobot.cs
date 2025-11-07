using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadRobot : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    public float visionRange;
    
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private float moveSpeed = 2f;

    private int waypointIndex = 0;

    // --- Walking Step Effect Variables ---
    [SerializeField] private float bobIntensity = 0.1f; 
    [SerializeField] private float stepCycleLength = 1.0f; // Distance traveled for one full step cycle
    
    private float distanceTraveled = 0f; 
    private Vector3 lastPosition; 

    void Start()
    {
        if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[waypointIndex].transform.position;
            lastPosition = transform.position; // Initialize last position
        }
    }

    void Update()
    {  
        animator.SetFloat("ForwardSpeed", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        // Debug.Log(navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        //print(navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        //print(transform.position);
        //print(player.transform.position);
        //print(Vector3.Distance(transform.position, player.transform.position));
        if (Vector3.Distance(transform.position, player.transform.position) > visionRange)
        {
            Move();
        } else {
            navMeshAgent.SetDestination(player.transform.position);
        }
        
      
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[waypointIndex].transform.position;

            // --- 1. Movement and Distance Tracking ---
            
            // Move Horizontally toward the target's X-Z position, ignoring Y for now.
            // We use a flat target derived from the current waypoint position.
            Vector3 flatTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            Vector3 previousPos = transform.position;
            
            // Perform the horizontal move
            transform.position = Vector3.MoveTowards(transform.position,
               flatTarget,
               moveSpeed * Time.deltaTime);

            // Calculate the distance moved and update the tracker for bobbing
            // Use the movement that just occurred
            float frameDistance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(previousPos.x, 0, previousPos.z));
            distanceTraveled += frameDistance;
            
            // --- 2. Calculate and Apply Distance-Based Bobbing Effect ---

            // Calculate the phase (where we are in the step cycle)
            float bobPhase = (distanceTraveled / stepCycleLength) * 2 * Mathf.PI;
            
            // Calculate the bob offset
            float bobOffset = Mathf.Sin(bobPhase) * bobIntensity;
            
            // Apply the bob to the Y position, using the TARGET'S Y as the baseline.
            Vector3 newPosWithBob = transform.position;
            newPosWithBob.y = targetPosition.y + bobOffset;
            transform.position = newPosWithBob;

            // --- 3. Check for Waypoint Arrival and Rotation ---

            // Check arrival using a flat distance check
            Vector3 currentPosFlat = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 targetPosFlat = new Vector3(targetPosition.x, 0, targetPosition.z);

            if (Vector3.Distance(currentPosFlat, targetPosFlat) < 0.05f) 
            {
                waypointIndex += 1;
                // Keep distanceTraveled to maintain step continuity across waypoints,
                // or reset it to 0 if you want a fresh step cycle at each waypoint.
                // We'll keep it running for a more continuous gait.

                // Reset to the first waypoint if the last waypoint is reached
                if (waypointIndex == waypoints.Length)
                {
                    waypointIndex = 0;
                }
            }

            // Rotation 
            if (waypointIndex < waypoints.Length)
            {
                Vector3 nextWaypointPos = waypoints[waypointIndex].transform.position;
                Vector3 direction2 = nextWaypointPos - transform.position;
                direction2.y = 0; // Ignore Y for rotation
                
                float angle = Mathf.Atan2(direction2.x, direction2.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
                transform.rotation = rotation;
            }
        }
        else // NPC reached the end of the path
        {
            // Lock the Y position to the final waypoint's Y to stop the bob
            if (waypoints.Length > 0)
            {
                 Vector3 finalWaypointPos = waypoints[waypoints.Length - 1].transform.position;
                 transform.position = new Vector3(transform.position.x, finalWaypointPos.y, transform.position.z);
            }
        }
    }
}
