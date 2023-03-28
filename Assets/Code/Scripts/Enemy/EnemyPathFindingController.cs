using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class EnemyPathFindingController : MonoBehaviour
{
    #nullable enable
    [SerializeField] private Transform? target;
    #nullable disable
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float agroTime;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private Path path;
    private Seeker seeker;

    private int currentWaypoint = 0;
    private float nextWaypointDistance = 3f;
    private bool canSeeTarget = false;
    private bool isChasingTarget = false;
    private bool isLosingAgro = false;
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
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

    private async void LoseAgro(float agroTime)
    {       
        isLosingAgro = true;
        await Task.Delay((int)(agroTime * 1000));
        if (this == null) return;
        isChasingTarget = false;
        CancelInvoke("GeneratePath");
        this.path = null;
        isLosingAgro = false;
    }

    public void CheckTarget()
    {   
        if (target == null)
        {
            CancelInvoke("GeneratePath"); 
            return;
        }
            

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, 18f, layerMask);

        canSeeTarget = hit.collider != null && hit.collider.CompareTag(target.tag) ? true : false;

        if (!canSeeTarget)
        {
            if (isChasingTarget)
            {
                if(!isLosingAgro)
                {
                    LoseAgro(agroTime);
                }                           
            }         
        }

        if (canSeeTarget)
        {
            if (!isChasingTarget)
            {
                InvokeRepeating("GeneratePath", 0f, 0.25f); //Calls the GeneratePath method on start and then every 0.25 seconds
                isChasingTarget = true;
            }
        }
            
        Color color = canSeeTarget ? Color.green : Color.red;
        Debug.DrawLine(transform.position, hit.point, color);    
    }

    public void FollowPath(float movementSpeed)
    {
        if (path == null) return;

        if (!isChasingTarget) return;

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

    public void HandleAnimation()
    {
        isMoving = Vector2.Distance(rigidBody.velocity, new Vector2(0, 0)) > 0.5  ? true : false;
        animator.SetBool("isMoving", isMoving);
    }
}
