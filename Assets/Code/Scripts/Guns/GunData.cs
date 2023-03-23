using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Gun Data")]
public class GunData : ScriptableObject
{  
    [Header("Gun Stats")]
    public new string name;

    public float damage;
    public float reloadSpeed;
    public float fireRate;

    public int magazineSize;

    public GunType gunType;

    [Header("Audio")]
    public AudioClip fireSFX;
    public AudioClip reloadSFX;
    public AudioClip emptyMagazineSFX;

    [Header("Bullet")]
    public GameObject bullet;

    [Header("Images")]
    public Sprite Sprite;
    public Sprite hudSprite;

    [Header("Equip position")]
    public Vector3 equipPosition;
}
