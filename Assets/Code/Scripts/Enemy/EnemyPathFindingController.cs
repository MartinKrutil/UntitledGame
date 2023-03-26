using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathFindingController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Rigidbody2D rigidBody;
    private Path path; 
    private Seeker seeker;
    
    private int currentWaypoint = 0;
    private float nextWaypointDistance = 3f;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();

        InvokeRepeating("GeneratePath", 0f, 0.25f); //Calls the GeneratePath method on start and then every 0.25 seconds
    }

    private void GeneratePath() => seeker.StartPath(rigidBody.position, target.position, OnPathGenerated);

    private void OnPathGenerated(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }

    public void CheckTarget()
    {
        if (target == null)
            CancelInvoke("GeneratePath");
    }

    public void FollowPath(float movementSpeed)
    {
        if (path == null) return;

        else if (currentWaypoint >= path.vectorPath.Count) return; //if current waypoint is greater than or equal to total amount of waypoint along the path

        Vector2 nextWaypointDirection = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized; //normalized vector2 direction from current position to next waypoint
        Vector2 force = nextWaypointDirection * movementSpeed * Time.fixedDeltaTime;

        rigidBody.AddForce(force);

        float currentDistance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]); //vector2 distance between current position and next waypoint

        if (currentDistance < nextWaypointDistance) currentWaypoint++;

        FaceMovementDirection(force.normalized);
    }

    private void FaceMovementDirection(Vector2 force)
    {
        if (force.x > 0f && rigidBody.velocity.x > 0f)
            transform.localScale = new Vector3(1f, 1f, 1f);

        else if (force.x < 0f && rigidBody.velocity.x < 0f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
