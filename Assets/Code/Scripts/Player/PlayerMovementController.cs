using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private Vector2 movementInput;

    private bool isMoving = false;

    #region Rotation

    private Vector2 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 targetDirection;
    private float rotationAngle;

    #endregion Rotation

    private void Start()
    {
        movementSpeed *= 100;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FollowCursor();
        HandleAnimation();
    }

    private void FixedUpdate() => Move();

    public void FollowCursor()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition); 
        targetDirection = mouseWorldPosition - transform.position; 

        rotationAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; 

        spriteRenderer.flipX = rotationAngle < -90 || rotationAngle > 90 ? true : false;    
    }

    public void HandleAnimation()
    {
        isMoving = movementInput != Vector2.zero ? true : false;
        animator.SetBool("isMoving", isMoving);
    }

    public void Move() => rigidBody.velocity = movementInput.normalized * movementSpeed * Time.fixedDeltaTime;

    private void OnMove(InputValue value) => movementInput = value.Get<Vector2>();
}
