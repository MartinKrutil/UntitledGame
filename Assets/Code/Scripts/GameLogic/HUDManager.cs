using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] private GunDisplay gunDisplay;
    [SerializeField] private HeartDisplay heartDisplay;
    [SerializeField] private TextMeshProUGUI winText;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        else
            instance = this;
    }

    public void SetGunDisplay(Gun gun)
    {
        gunDisplay.Enable();
        gunDisplay.SetAmmo(gun.currentAmmo, gun.gunData.magazineSize);
        gunDisplay.SetGunImage(gun.gunData.hudSprite);
    }

    public void DisableGunDisplay() => gunDisplay.Disable();

    public void UpdateAmmo(Gun gun) => gunDisplay.SetAmmo(gun.currentAmmo, gun.gunData.magazineSize);

    public void DisplayHearts(int heartCount) => heartDisplay.DisplayHearts(heartCount);

    public void EnableWinText() => winText.enabled = true;
}
