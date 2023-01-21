using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] GameObject sonde;

    public static EnemyManager instance;

    Vector3 giz1;
    Vector3 giz2;
    
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

    public Enemy GetCloseEnemy(Satellite satellite)
    {
        if (enemyList.Count == 0) return null;

        Enemy closestEnemy = null;
        float distance = 999999;

        foreach (Enemy enemy in enemyList)
        {
            
            if (enemy != null)
            {
                float distanceToCheck = (enemy.transform.position - satellite.transform.position).magnitude;
                if (distanceToCheck < distance)
                {
                    distance = distanceToCheck;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(giz1, giz2);
    }
}
