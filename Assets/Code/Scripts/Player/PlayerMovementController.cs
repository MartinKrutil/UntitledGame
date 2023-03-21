using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private Vector2 movementInput;

    #region Rotation

    private Vector2 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 targetDirection;
    private float rotationAngle;

    #endregion Rotation

    void Start()
    {
        movementSpeed *= 100;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FollowCursor()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue(); //Position of cursor on screen
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition); //Converts mouse cursor screen position to world (game) position
        targetDirection = mouseWorldPosition - transform.position; //Direction from current object position to the mouse world position

        rotationAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; //Angle of rotation required to point object towards the mouse position in degrees

        spriteRenderer.flipX = rotationAngle < -90 || rotationAngle > 90 ? true : false; //Flips sprite on x axis depending on which side the cursor is relative to the sprite      
    }
    
    public void Move() => rigidBody.velocity = movementInput.normalized * movementSpeed * Time.fixedDeltaTime;

    private void OnMove(InputValue value) => movementInput = value.Get<Vector2>();
}
