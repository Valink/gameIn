
using UnityEngine;

public class EnnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject explosionFX;
    [SerializeField] float speed;

    public void Init(GameObject target)
    {
        this.target = target;
        NavigationHelper.LookToward(transform, target.transform.position);
    }

    public void Update()
    {
        NavigationHelper.MoveToward(transform, target.transform.position, speed);
    }

    public void Destroy()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
