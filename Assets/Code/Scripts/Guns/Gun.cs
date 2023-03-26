using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Transform firePoint;

    public int currentAmmo;

    public bool isEquipable = false;

    private void Start() => currentAmmo = gunData == null ? 0 : gunData.magazineSize; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isEquipable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isEquipable = false;
    }
}
