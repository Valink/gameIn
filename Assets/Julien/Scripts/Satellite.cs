using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    enum Order { Desactivated, Attack, Rapatrier }
    [SerializeField] Order orderActual;

    [SerializeField] float speed;

    [SerializeField] GameObject sonde;

    [SerializeField] GameObject enemy;



    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        orderActual = Order.Desactivated;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) orderActual = Order.Rapatrier;
        if (Input.GetKeyDown(KeyCode.A)) orderActual = Order.Attack;

        Rapatrier();
        Attack();
        Desactivated();
    }

    public void Rapatrier()
    {
        if (orderActual != Order.Rapatrier) return;

        transform.rotation = Quaternion.LookRotation(transform.forward, transform.position - sonde.transform.position);
        rb.velocity = -transform.up * speed * Time.deltaTime;
    }

    public void Attack()
    {
        if (orderActual != Order.Attack || enemy == null) return;

        transform.rotation = Quaternion.LookRotation(transform.forward, transform.position - enemy.transform.position);
        rb.velocity = -transform.up * speed * Time.deltaTime;
    }

    public void Desactivated()
    {
        if (orderActual != Order.Desactivated) return;

        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sonde")
        {
            orderActual = Order.Desactivated;
        }

        if (collision.tag == "Enemy")
        {
            orderActual = Order.Desactivated;
            Destroy(enemy);
        }
    }
}
