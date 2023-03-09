using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Gun Data")]
public class GunData : ScriptableObject
{
    public new string name;

    public float damage;
    
    public float reloadSpeed;
    public float fireRate;

    public int clipSize;

    public GunType gunType;
    public BulletType BulletType;

    public GameObject bullet;
}
