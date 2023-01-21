using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionFX;

    public void Explode()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
