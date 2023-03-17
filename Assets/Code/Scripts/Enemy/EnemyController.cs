using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{ 
    [SerializeField]
    private float movementSpeed = 1000f;

    [SerializeField]
    private EnemyPathFindingController pathFinding;

    private void Start() => movementSpeed *= 100;

    private void FixedUpdate()
    {
        pathFinding.CheckTarget();
        pathFinding.FollowPath(movementSpeed);       
    }  
}
