using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private List<UnitBehaviour> hiredUnits;

    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void TriggerAction(string action)
    {
        switch (action.ToUpper())
        {
            case "ATTACK":
                foreach (UnitBehaviour unit in hiredUnits)
                {
                    if (unit.state == UnitBehaviour.State.Hired)
                    {
                        unit.Attack();
                    }
                }
                break;
            case "RESTART":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "START":
                SpawnerBehaviour.Instance.StartGame();
                LightManager.instance.StartGame();
                GameManager.Instance.StartGame();
                break;
            default:
                break;
        }
    }

    public void HireUnit(string UnitBehaviourName)
    {
        var sb = SpawnerBehaviour.Instance;
        var u = sb.GetUnitByName(UnitBehaviourName);
        sb.spawnedUnits.Remove(u);

        var ub = u.GetComponent<UnitBehaviour>();
        hiredUnits.Add(ub);
        
        ub.JoinFirstTime();
    }

    internal void Hit()
    {
        health--;
        SetVisualForHealth(health);
    }

    private void SetVisualForHealth(int health)
    {
        switch (health)
        {
            case 2:
                spriteRenderer.color = Color.yellow;
                break;
            case 1:
                spriteRenderer.color = Color.red;
                break;
            case 0:
                // Instantiate(explosionFX, transform.position, Quaternion.identity); // TODO Julien
                GameManager.Instance.GameOver();
                break;
            default:
                break;
        }
    }
}
