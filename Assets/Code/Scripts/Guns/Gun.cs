using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Transform firePoint;

    public int ID;
    public bool isEquipable = false;

    private void Start()
    {
        //ID = GunManager.instance.guns.IndexOf(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isEquipable = true;

        //print($"{gameObject.name}, is equipable: {isEquipable}");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isEquipable = false;

        //print($"{gameObject.name}, is equipable: {isEquipable}");
    }
}
