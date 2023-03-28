using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<GameObject> enemies;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        else
            instance = this;

        LoadEnemies();
    }

    public void Update()
    {
        if (!enemies.Any())
            HUDManager.instance.EnableWinText();
    }

    private void LoadEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf)
                enemies.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UpdateEnemies()
    {
        enemies.Clear();
        LoadEnemies();
    }
}
