using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifetime);
    }

    private void FixedUpdate()
    {
        rb.velocity=transform.up*speed;

    }

    public void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
