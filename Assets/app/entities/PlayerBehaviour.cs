using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private List<UnitBehaviour> hiredUnits;

    public void TriggerAction(string action)
    {
        switch (action.ToUpper())
        {
            case "ATTACK":
                foreach (UnitBehaviour unit in hiredUnits)
                {
                    unit.Attack();
                }
                break;
            default:
                break;
        }
    }
    
    public void HireUnit(string UnitBehaviourName)
    {
        var sb = SpawnerBehaviour.Instance;
        var u = sb.GetUnitByName(UnitBehaviourName);
        Debug.Log(u);
        var ub = u.GetComponent<UnitBehaviour>();
        Debug.Log(ub);
        hiredUnits.Add(ub);
        ub.Join();
    }
}
