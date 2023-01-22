
using UnityEngine;

public class EnnemyBehaviour : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceDie;
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
    
    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.tag == "Player")
        {
            collided.GetComponent<PlayerBehaviour>().Hit();
            Destroy();
        }
    }

    public void Destroy()
    {
        AudioManager.instance.PlayExplode();
        Instantiate(explosionFX, transform.position, Quaternion.identity); // TODO Julien fix
        Destroy(gameObject); // TODO destroy after animation and sound
        
    }
}
