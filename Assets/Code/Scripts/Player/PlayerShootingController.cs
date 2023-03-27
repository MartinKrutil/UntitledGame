using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerShootingController : MonoBehaviour
{
    #region Fields

#nullable enable
    private GameObject? gun = null;
#nullable disable

    [SerializeField] private ScreenShake screenShaker;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private LayerMask layerMasks;

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
        if (this.gun != null)
        {
            FollowCursor();
            if (inputValue > 0) Shoot();
        }
    }

    #region Methods

    #region Movement

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

    #endregion Movement

    #region Shooting

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

        ShootByRaycast();
        gunScript.currentAmmo--;

        //screenShaker.Shake();
        animator.SetTrigger("Shoot");        
        SoundManager.instance.PlaySound(gunScript.gunData.fireSFX);
        HUDManager.instance.UpdateAmmo(gunScript);

        if (gunScript.gunData.shootingType == ShootingType.SemiAutomatic) inputValue = 0;
    }

    private void ShootByRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, targetDirection.normalized, 1000f, layerMasks);
        //Debug.DrawLine(firePoint.position, hit.point, Color.red); 

        if (hit)
        {
            GameObject trail = Instantiate(bulletTrail, firePoint.position, firePoint.rotation);
            if (trail.TryGetComponent<BulletTrail>(out BulletTrail bulletTrailScript))
                bulletTrailScript.ShootBullet(hit, gunScript.gunData.damage);
        }          
    }

    private void ShootBulletPrefab()
    {
        GameObject bullet = Instantiate(gunScript.gunData.bullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().SetDamage(gunScript.gunData.damage);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(targetDirection.x, targetDirection.y).normalized * 40f;
    }

    private async void Reload()
    {
        if(this.gun == null) return;

        isReloading = true;
        SoundManager.instance.PlaySound(gunScript.gunData.reloadSFX);

        await Task.Delay((int)(gunScript.gunData.reloadSpeed * 1000));

        gunScript.currentAmmo = gunScript.gunData.magazineSize;
        HUDManager.instance.UpdateAmmo(gunScript);
        isReloading = false;
    }

    #endregion Shooting

    #region Gun Handling

    private void HandleItem()
    {
        if (this.gun != null) DropGun(this.gun);

        foreach (GameObject gun in GunManager.instance.guns)
        {
            if (gun.GetComponent<Gun>().isEquipable)
            {
                EquipGun(gun);
                break;
            }
        }
    }

    private void EquipGun(GameObject gun)
    {       
        gun.GetComponent<BoxCollider2D>().enabled = false;
        gun.GetComponent<CapsuleCollider2D>().enabled = false;
        gun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gun.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gun.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        gun.GetComponent<SpriteRenderer>().sortingLayerName = "GunEquipped";

        SetGun(gun);

        GunManager.instance.MoveGunToHands(gun, transform);
        HUDManager.instance.SetGunDisplay(gunScript);
        SoundManager.instance.PlaySound(gunScript.gunData.reloadSFX);
    }

    private void DropGun(GameObject gun)
    {       
        gun.GetComponent<BoxCollider2D>().enabled = true;
        gun.GetComponent<CapsuleCollider2D>().enabled = true;
        gun.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        gun.GetComponent<SpriteRenderer>().sortingLayerName = "GunOnGround";

        ThrowGun(gun);

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

    private void ThrowGun(GameObject gun)
    {
        Rigidbody2D rigidbody = gun.GetComponent<Rigidbody2D>();
        float rotationVelocity = 360f * Random.Range(4f, 8f);
        rigidbody.velocity = new Vector2(targetDirection.x, targetDirection.y).normalized * 50;
        rigidbody.angularVelocity = rotationAngle < -90 || rotationAngle > 90 ? rotationVelocity : -rotationVelocity;
    }

    #endregion Gun Handling

    #endregion Methods

    #region Input

    private void OnFire(InputValue value) => inputValue = value.Get<float>();
    private void OnReload() => Reload();
    private void OnItemInteraction() => HandleItem();

    #endregion Input
}
