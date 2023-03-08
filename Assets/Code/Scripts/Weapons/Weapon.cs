using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;

    public int damage;
    public int clipSize;

    public float reloadSpeed;
    public float shootingSpeed; 

    public GunType gunType;
    public AmmoType ammoType;

    public Transform firePoint;
    public GameObject ammo;

    public Sprite sprite;
    
}
