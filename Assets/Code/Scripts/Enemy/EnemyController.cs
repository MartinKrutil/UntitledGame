using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{  
    [SerializeField] private EnemyPathFindingController pathFinding;

    [SerializeField] private float movementSpeed = 1000f;

    private void Start() => movementSpeed *= 100;

    private void FixedUpdate()
    {
        pathFinding.CheckTarget();
        pathFinding.FollowPath(movementSpeed);       
    }  
}
