using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private Gun gun;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private AudioSource fireSFX;

    #endregion Properties

    #region Rotation

    private Vector2 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 targetDirection;
    private float rotationAngle;

    #endregion Rotation

    private void Start() { }

    private void Update()
    {
        FollowCursor();
    }

    #region Methods

    /// <summary>
    /// Rotates gun towards cursor position
    /// </summary>
    private void FollowCursor()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue(); //Position of cursor on screen
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition); //Converts mouse cursor screen position to world (game) position
        targetDirection = mouseWorldPosition - transform.position; //Direction from current object position to the mouse world position

        rotationAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; //Angle of rotation required to point object towards the mouse position in degrees

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle)); //Rotates object on z axis towards the cursor

        //Rotates the object by 180 degrees depending on which side the cursor is relative to the object
        if (rotationAngle < -90 || rotationAngle > 90)
            transform.localRotation = Quaternion.Euler(new Vector3(180, 0, -rotationAngle));
    }

    /// <summary>
    /// Shoots bullet from fire point by adding force to its rigidbody
    /// </summary>
    private void Shoot()
    {
        GameObject bullet = Instantiate(gun.gunData.bullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(targetDirection.x, targetDirection.y).normalized * 40f;
        fireSFX.Play();
    }

    #endregion Methods

    #region Input

    private void OnFire() => Shoot();

    #endregion Input
}
