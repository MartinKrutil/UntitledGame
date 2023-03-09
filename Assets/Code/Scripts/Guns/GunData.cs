using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Gun Data")]
public class GunData : ScriptableObject
{
    public new string name;

    public int damage;
    public int clipSize;

    public float reloadSpeed;
    public float shootingSpeed;

    public GunType gunType;
    public BulletType BulletType;

    public GameObject bullet;
}
