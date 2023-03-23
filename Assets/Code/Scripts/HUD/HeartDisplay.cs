using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    [SerializeField] private GameObject heart;

    public void DisplayHearts(int heartCount)
    {
        DestroyHearts();
        for (int i = 0; i < heartCount; i++)
        {
            GameObject heart = Instantiate(this.heart);
            heart.transform.SetParent(transform);
        }     
    }

    public void DestroyHearts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy((transform.GetChild(i).gameObject));
        }
    }
}
