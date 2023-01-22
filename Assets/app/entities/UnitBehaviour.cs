
using TMPro;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    enum State { Deriving, Waiting, Joining, Hired, Attacking }
    [SerializeField] private State state;
    [SerializeField] private GameObject target;

    [SerializeField] private TMP_Text nameText;
    private Rigidbody2D rb;

    [SerializeField] float speed;

    [SerializeField] private GameObject spriteGO;
    [SerializeField] float derivingAndWaitingRotationSpeed;

    [SerializeField] private GameObject master;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hiredSprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(GameObject player)
    {
        nameText.text = name;
        master = player;
        state = State.Deriving;
    }

    void Update()
    {
        switch (state)
        {
            case State.Deriving:
                NavigationHelper.MoveToward(transform, master.transform.position, speed);
                DerivingAndWaitingRotate();
                break;
            case State.Waiting:
                DerivingAndWaitingRotate();
                break;
            case State.Joining:
                NavigationHelper.MoveToward(transform, master.transform.position, speed);
                break;
            case State.Hired:
                break;
            case State.Attacking:
                NavigationHelper.LookToward(spriteGO.transform, target.transform.position);
                NavigationHelper.MoveToward(transform, target.transform.position, speed);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (state == State.Deriving && collided.tag == "StopUnitsZone")
        {
            state = State.Waiting;
        }

        if (state == State.Joining && collided.tag == "Player")
        {
            state = State.Hired;
            spriteRenderer.sprite = hiredSprite;
            spriteGO.transform.localRotation = Quaternion.Euler(0, 0, -180); // TODO
        }

        if (state == State.Attacking && collided.gameObject == target)
        {
            target.GetComponent<EnnemyBehaviour>().Destroy();
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        state = State.Attacking;
        target = SpawnerBehaviour.Instance.GetClosestEnnemyTo(this);
    }

    public void Join()
    {
        state = State.Joining;
        NavigationHelper.LookToward(spriteGO.transform, master.transform.position);
        NavigationHelper.MoveToward(transform, master.transform.position, speed);
    }

    private void DerivingAndWaitingRotate()
    {
        spriteGO.transform.Rotate(new Vector3(0, 0, derivingAndWaitingRotationSpeed));
    }
}
