using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    #region Properties

    SpriteRenderer spriteRenderer;

    Rigidbody2D rigidBody;

    Vector2 movementInput;

    public float movementSpeed = 1f;

    #endregion Properties

    private Vector2 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 targetDirection;
    private float rotationAngle;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
        FollowCursor();
    }

    #region Methods

    /// <summary>
    /// Makes player face the cursor
    /// </summary>
    void FollowCursor()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue(); //Position of cursor on screen
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition); //Converts mouse cursor screen position to world (game) position
        targetDirection = mouseWorldPosition - transform.position; //Direction from current object position to the mouse world position

        rotationAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; //Angle of rotation required to point object towards the mouse position in degrees

        spriteRenderer.flipX = rotationAngle < -90 || rotationAngle > 90 ? true : false; //Flips sprite on x axis depending on which side the cursor is relative to the sprite
    }

    public void Move() => rigidBody.velocity = movementInput.normalized * movementSpeed;

    #endregion Methods

    #region Input

    void OnMove(InputValue value) => movementInput = value.Get<Vector2>();

    #endregion Input

}
