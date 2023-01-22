using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void Update()
    {
        NavigationHelper.LookToward(transform, target.transform.position);
    }
}
