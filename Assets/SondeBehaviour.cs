using System.Collections.Generic;
using UnityEngine;

public class SondeBehaviour : MonoBehaviour
{
    [SerializeField] private List<Satellite> friends;
    [SerializeField] private List<Satellite> enrolledFriends;

    internal void AddFriend(Satellite s)
    {
        friends.Add(s);
    }

    public void TriggerAction(string action)
    {
        Debug.Log($"Action: {action}");

        switch (action)
        {
            case "ATTACK":
                foreach (Satellite enrolledFriend in enrolledFriends)
                {
                    enrolledFriend.InputAttack();
                }
                break;
            default:
                break;
        }

    }
    
    public void EnrollFriend(string friendName)
    {
        Debug.Log($"Satellite: {friendName}");
        var a = friends.Find(f => f.name == friendName);
        Debug.Log(a);
        var b = a.GetComponent<Satellite>();
        Debug.Log(b);
        a.InputRapatrier();
    }
}
