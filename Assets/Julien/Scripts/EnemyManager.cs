using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] GameObject sonde;

    public static EnemyManager instance;
    
    void Awake()
    {
        if (instance == null) instance = this;

        enemyList.AddRange(GameObject.FindObjectsOfType<Enemy>());
    }

    public void NewEnemy(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public Enemy GetCloseEnemy()
    {
        if (enemyList.Count == 0) return null;

        Enemy closestEnemy = null;
        float distance = 999999;

        foreach (Enemy enemy in enemyList)
        {
            if (enemy == null) break;
            float distanceToCheck = Mathf.Abs(sonde.transform.position.magnitude - enemy.transform.position.magnitude);
            if (distanceToCheck < distance)
            {
                distance = distanceToCheck;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
