using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    #region Fields

    #nullable enable
    private GameObject? gun = null;
    #nullable disable

    [SerializeField] private ScreenShake screenShaker;

    private BoxCollider2D boxCollider;

    private Gun gunScript;
    private Animator animator;
    private Transform firePoint;

    private bool isReloading = false;
    private float shootTimer = 0f;
    private float inputValue = 0f;

    #endregion Fields

    #region Rotation

    private Vector2 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 targetDirection;
    private float rotationAngle;

    #endregion Rotation

    private void Update()
    {
        if (gun != null)
        {
            FollowCursor();
            if (inputValue > 0) Shoot();
        }
    }

    #region Methods

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

    private void Shoot()
    {
        if (isReloading) return;

        if (Time.time < shootTimer) return;
        shootTimer = Time.time + 1 / gunScript.gunData.fireRate;

        if (gunScript.currentAmmo <= 0)
        {
            SoundManager.instance.PlaySound(gunScript.gunData.emptyMagazineSFX);
            return;
        }

        ShootBullet();
        gunScript.currentAmmo--;

        animator.SetTrigger("Shoot");
        //screenShaker.Shake();
        SoundManager.instance.PlaySound(gunScript.gunData.fireSFX);
        HUDManager.instance.UpdateAmmo(gunScript);     
       
        if (gunScript.gunData.gunType == GunType.SemiAutomatic) inputValue = 0;
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(gunScript.gunData.bullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(targetDirection.x, targetDirection.y).normalized * 40f;
    }

    private async void Reload()
    {
        isReloading = true;
        SoundManager.instance.PlaySound(gunScript.gunData.reloadSFX);

        await Task.Delay((int)(gunScript.gunData.reloadSpeed * 1000));

        gunScript.currentAmmo = gunScript.gunData.magazineSize;
        HUDManager.instance.UpdateAmmo(gunScript);
        isReloading = false;
    }

    private void HandleItem()
    {
        if(gun != null) DropGun(gun);

        foreach (GameObject item in GunManager.instance.guns)
        {
            if (item.GetComponent<Gun>().isEquipable)
            {
                EquipGun(item);
                break;
            }
        }
    }

    private void EquipGun(GameObject gun)
    {
        SetGun(gun);
        gun.GetComponent<BoxCollider2D>().enabled = false;

        gun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        //gun.GetComponent<Rigidbody2D>().Sleep();

        GunManager.instance.MoveGunToHands(gun, transform);
        HUDManager.instance.SetGunDisplay(gunScript);      
        SoundManager.instance.PlaySound(gunScript.gunData.reloadSFX);
    }

    private void DropGun(GameObject gun)
    {
        gun.GetComponent<BoxCollider2D>().enabled = true;
        //gun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //gun.GetComponent<Rigidbody2D>().AddForce(targetDirection.normalized * 20, ForceMode2D.Impulse);
        //gun.GetComponent<Rigidbody2D>().AddTorque(45, ForceMode2D.Force);

        GunManager.instance.MoveGunToList(gun);
        HUDManager.instance.DisableGunDisplay();

        this.gun = null;              
    }

    private void SetGun(GameObject gun)
    {
        this.gun = gun;
        gunScript = gun.GetComponent<Gun>();
        animator = gun.GetComponent<Animator>();
        firePoint = gunScript.firePoint;
    }

    #endregion Methods

    #region Input

    private void OnFire(InputValue value) => inputValue = value.Get<float>();
    private void OnReload() => Reload();
    private void OnItemInteraction() => HandleItem();

    #endregion Input
}
