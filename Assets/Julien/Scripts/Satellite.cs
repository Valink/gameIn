using TMPro;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    enum Order { Desactivated, DesactivatedMove, Idle, Attack, Rapatrier, Aimante }
    [SerializeField] Order orderActual;

    [SerializeField] float speed;

    [SerializeField] GameObject sonde;

    [SerializeField] Sprite spriteDesactivated;

    [SerializeField] Sprite spriteActivated;

    [SerializeField] float desactivateRotation;

    SpriteRenderer spriteR;
    GameObject spriteGMB;

    Enemy enemy;

    bool isActif = false;

    [SerializeField] private TMP_Text nameText;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
        spriteGMB = spriteR.gameObject; ;
        orderActual = Order.DesactivatedMove;
    }

    public void Init(GameObject player)
    {
        nameText.text = name;
        sonde = player;
        LookToward(sonde);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) orderActual = Order.Rapatrier;
        if (Input.GetKeyDown(KeyCode.A) && isActif) orderActual = Order.Attack;

        Rapatrier();
        Attack();
        Desactivated();
        DesactivatedMove();
        Idle();
        Aimante();


    }

    public void DesactivatedMove()
    {
        if (orderActual != Order.DesactivatedMove) return;
        
        transform.position = Vector3.Lerp(transform.position, Vector3.zero, 0.1f * Time.deltaTime);

        spriteGMB.transform.Rotate(new Vector3(0, 0, desactivateRotation));
    }

    public void Activate()
    {
        if (!isActif) return;

        spriteGMB.transform.localRotation = Quaternion.Euler(0, 0, 0);
        spriteR.sprite = spriteActivated;
    }

    public void Rapatrier()
    {
        if (orderActual != Order.Rapatrier) return;

        LookToward(sonde);
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    public void LookToward(GameObject target)
    {
        transform.rotation = Quaternion.LookRotation(transform.forward, target.transform.position - transform.position);
    }

    public void Attack()
    {
        if (orderActual != Order.Attack) return;

        enemy = EnemyManager.instance.GetCloseEnemy(this);

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

        spriteGMB.transform.Rotate(new Vector3(0, 0, desactivateRotation));
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sonde" && orderActual == Order.Rapatrier)
        {
            orderActual = Order.Idle;
            isActif = true;
            Activate();
        }

        if (collision.tag == "stopZone" && orderActual == Order.DesactivatedMove)
        {
            orderActual = Order.Desactivated;
        }

        if (collision.tag == "Enemy" && orderActual == Order.Attack)
        {
            EnemyManager.instance.RemoveEnemy(enemy);
            enemy.Explode();
            Destroy(enemy.gameObject);
            Destroy(this.gameObject);
        }
    }
}
