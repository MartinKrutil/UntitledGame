using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public List<GameObject> items;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        else
            instance = this;
    }

    private void Start()
    {
        LoadItems();
        PrintItems(items);
    }

    private void Update()
    {

    }

    private void LoadItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            items.Add(transform.GetChild(i).gameObject);
        }
    }

    private void PrintItems(List<GameObject> items)
    {
        foreach (var item in items)
        {
            print(item.name);
        }
    }

    public void MoveItem(Transform parent)
    {
        items[0].transform.parent = parent;
    }

    public void MoveItem(GameObject item, Transform parent)
    {

    }
}
