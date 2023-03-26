using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerHealthController healthController;
    [SerializeField] private PlayerMovementController movementController;

    private void Update() => movementController.FollowCursor();

    private void FixedUpdate() => movementController.Move();
}
