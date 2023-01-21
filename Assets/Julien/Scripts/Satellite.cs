using TMPro;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    enum Order { Desactivated, Idle, Attack, Rapatrier, Aimante }
    [SerializeField] Order orderActual;

    [SerializeField] float speed;

    [SerializeField] GameObject sonde;

    [SerializeField] Sprite spriteDesactivated;

    [SerializeField] Sprite spriteActivated;

    SpriteRenderer spriteR;

    Enemy enemy;

    bool isActif = false;

    [SerializeField] private TMP_Text nameText;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        orderActual = Order.Desactivated;
    }

    public void Init()
    {
        nameText.text = name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) orderActual = Order.Rapatrier;
        if (Input.GetKeyDown(KeyCode.A)) orderActual = Order.Attack;

        Rapatrier();
        Attack();
        Desactivated();
        Idle();
        Aimante();
    }

    public void Activate()
    {
        if (!isActif) return;

        spriteR.sprite = spriteActivated;
    }

    public void Rapatrier()
    {
        if (orderActual != Order.Rapatrier) return;

        isActif = true;
        Activate();
        transform.rotation = Quaternion.LookRotation(transform.forward, sonde.transform.position - transform.position);
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    public void Attack()
    {
        if (orderActual != Order.Attack) return;

        enemy = EnemyManager.instance.GetCloseEnemy();

        if (enemy == null)
        {
            orderActual = Order.Idle;
            return;
        }

        transform.rotation = Quaternion.LookRotation(transform.forward, enemy.transform.position - transform.position);
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    public void Desactivated()
    {
        if (orderActual != Order.Desactivated) return;

        spriteR.sprite = spriteDesactivated;
        rb.velocity = Vector2.zero;
    }

    public void Idle()
    {
        if (orderActual != Order.Idle) return;

        rb.velocity = Vector2.zero;
    }

    public void Aimante()
    {
        if (orderActual != Order.Aimante) return;

        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sonde" && orderActual == Order.Rapatrier)
        {
            orderActual = Order.Idle;
        }

        if (collision.tag == "Enemy" && orderActual == Order.Attack)
        {
            EnemyManager.instance.RemoveEnemy(enemy);
            enemy.Explode();
            Destroy(enemy.gameObject);
        }
    }
}
