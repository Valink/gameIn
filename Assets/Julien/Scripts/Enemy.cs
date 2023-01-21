using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject explosionFX;

    public void Explode()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void Init(GameObject player)
    {
        this.player = player;
    }

    public void Update()
    {
        MoveToward(player);
        LookToward(player);
    }

    public void MoveToward(GameObject target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 1f * Time.deltaTime);
    }

    public void LookToward(GameObject target)
    {
        transform.rotation = Quaternion.LookRotation(transform.forward, target.transform.position - transform.position);
    }
}
