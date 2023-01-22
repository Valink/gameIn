using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            default:
                break;
        }
    }

    public void HireUnit(string UnitBehaviourName)
    {
        var ub = SpawnerBehaviour.Instance.GetUnitByName(UnitBehaviourName).GetComponent<UnitBehaviour>();
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
