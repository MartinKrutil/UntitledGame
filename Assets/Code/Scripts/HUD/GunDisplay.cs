using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammo;
    [SerializeField] private Image gunImage;

    private void Start()
    {
        ammo.enabled = false;
        gunImage.enabled = false;
    }

    public void SetGunImage(Sprite gunImage) => this.gunImage.sprite = gunImage;
    public void SetAmmo(int currentAmmo, int maxAmmo) => ammo.text = $"{currentAmmo} | {maxAmmo}";
    public void Disable()
    {
        ammo.enabled = false;
        gunImage.enabled = false;
    }
    public void Enable()
    {
        ammo.enabled = true;
        gunImage.enabled = true;
    }
}
