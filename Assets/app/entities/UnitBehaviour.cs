
using TMPro;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceContacted;
    [SerializeField] private AudioSource audioSourceHired;
    [SerializeField] private AudioSource audioSourceAttack;

    public enum State { Deriving, Waiting, Joining, Hired, Attacking }
    [SerializeField] public State state;
    [SerializeField] private GameObject target;

    [SerializeField] private TMP_Text nameText;
    private Rigidbody2D rb;

    [SerializeField] float derivatingSpeed;
    [SerializeField] float attackSpeed;

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
        if (GameManager.Instance.GetGameOverState()) return;

        switch (state)
        {
            case State.Deriving:
                NavigationHelper.MoveToward(transform, master.transform.position, derivatingSpeed);
                DerivingAndWaitingRotate();
                break;
            case State.Waiting:
                DerivingAndWaitingRotate();
                break;
            case State.Joining:
                NavigationHelper.MoveToward(transform, master.transform.position, attackSpeed);
                break;
            case State.Hired:
                break;
            case State.Attacking:
                if (target != null)
                {
                    NavigationHelper.LookToward(spriteGO.transform, target.transform.position);
                    NavigationHelper.MoveToward(transform, target.transform.position, attackSpeed);
                }
                else
                {
                    target = SpawnerBehaviour.Instance.GetClosestEnnemyTo(this);
                    if (target == null)
                    {
                        Join();
                    }
                }
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
            audioSourceHired.Play();
        }

        if (state == State.Attacking && collided.gameObject == target)
        {
            SpawnerBehaviour.Instance.spawnedUnits.Remove(gameObject);
            SpawnerBehaviour.Instance.spawnedEnnemies.Remove(target);

            target.GetComponent<EnnemyBehaviour>().Destroy();
            Destroy(gameObject);

            GameManager.Instance.IncrementScore();
        }
    }

    public void Attack()
    {
        state = State.Attacking;
        target = SpawnerBehaviour.Instance.GetClosestEnnemyTo(this);
        audioSourceAttack.Play();
    }

    public void Join()
    {
        nameText.gameObject.SetActive(false);
        state = State.Joining;
        NavigationHelper.LookToward(spriteGO.transform, master.transform.position);
    }

    public void JoinFirstTime()
    {
        Join();
        audioSourceContacted.Play();
    }

    private void DerivingAndWaitingRotate()
    {
        spriteGO.transform.Rotate(new Vector3(0, 0, derivingAndWaitingRotationSpeed));
    }
}
