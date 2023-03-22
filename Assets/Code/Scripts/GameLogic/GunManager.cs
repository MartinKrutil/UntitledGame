using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;

    public List<GameObject> guns;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        else
            instance = this;

        LoadGuns();
    }

    private void Start()
    {       
    }

    private void Update()
    {

    }

    private void LoadGuns()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            guns.Add(transform.GetChild(i).gameObject);
        }
    }

    private void PrintGuns(List<GameObject> guns)
    {
        foreach (var item in guns)
        {
            print(item.name);
        }
    }

    public void MoveGunToHands(GameObject gun, Transform parent, float rotationAngle)
    {
        gun.transform.parent = parent;
        gun.transform.localPosition = gun.GetComponent<Gun>().gunData.equipPosition;
        gun.transform.localRotation = Quaternion.identity;
        guns.Remove(gun);
    }
    public void MoveGunToList(GameObject gun)
    {
        gun.transform.parent = transform;
        gun.transform.localPosition = gun.transform.localPosition;
        guns.Add(gun);
    }
}
