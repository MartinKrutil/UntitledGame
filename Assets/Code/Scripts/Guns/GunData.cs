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

    public int magazineSize;

    public GunType gunType;

    public AudioClip fireSFX;
    public AudioClip reloadSFX;
    public AudioClip emptyMagazineSFX;

    public GameObject bullet;

    public Sprite Sprite;
    public Sprite hudSprite;

    public Vector3 equipPosition;
}
