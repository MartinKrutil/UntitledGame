using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class EnemyPathFindingController : MonoBehaviour
{
#   nullable enable
    [SerializeField] private Transform? target;
#   nullable disable

    [SerializeField] private float movementSpeed = 1000f;
    [SerializeField] private float agroTime;
    [SerializeField] private LayerMask layerMask; 

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
        movementSpeed *= 100;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
    }

    private void Update()
    {
        CheckTarget();
        HandleAnimation();
    }

    private void FixedUpdate() => FollowPath(movementSpeed);

    private void GeneratePath() => seeker.StartPath(rigidBody.position, target.position, OnPathGenerated);

    private void OnPathGenerated(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }

    private void CheckTarget()
    {   
        if (target == null)
        {
            CancelInvoke("GeneratePath"); 
            return;
        }    

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, 18f, layerMask);

        canSeeTarget = hit.collider != null && hit.collider.CompareTag(target.tag) ? true : false;

        if (!canSeeTarget && isChasingTarget && !isLosingAgro)
            LoseAggro(agroTime);

        if (canSeeTarget && !isChasingTarget)
        {
            InvokeRepeating("GeneratePath", 0f, 0.25f);
            isChasingTarget = true;
        }             
    }
    private async void LoseAggro(float agroTime)
    {
        isLosingAgro = true;

        await Task.Delay((int)(agroTime * 1000));

        if (this == null) return;

        CancelInvoke("GeneratePath");

        this.path = null;

        isChasingTarget = false;
        isLosingAgro = false;
    }

    private void FollowPath(float movementSpeed)
    {
        if (path == null) return;

        if (!isChasingTarget) return;

        else if (currentWaypoint >= path.vectorPath.Count) return; 

        Vector2 nextWaypointDirection = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized; 
        Vector2 force = nextWaypointDirection * movementSpeed * Time.fixedDeltaTime;

        rigidBody.AddForce(force);

        float currentDistance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);

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

    private void HandleAnimation()
    {
        isMoving = Vector2.Distance(rigidBody.velocity, new Vector2(0, 0)) > 0.5  ? true : false;
        animator.SetBool("isMoving", isMoving);
    }
}
