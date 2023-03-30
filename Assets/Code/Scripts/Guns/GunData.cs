using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Gun Data")]
public class GunData : ScriptableObject
{
    #region Gun Data

    [Header("Gun Stats")]
    public new string name;

    public float damage, reloadSpeed, fireRate;

    public int magazineSize;

    public GunType gunType;
    public ShootingType shootingType;
    public Rarity rarity;

    [Header("Audio")]
    public AudioClip fireSFX, reloadSFX, emptyMagazineSFX;

    [Header("Bullet")]
    public GameObject bullet;

    [Header("Images")]
    public Sprite Sprite, hudSprite;

    [Header("Equip position")]
    public Vector3 equipPosition;

    #endregion Gun Data

    public void GetRandomData()
    {
        this.name = $"{Random.Range(0,1)}";
    }
}
