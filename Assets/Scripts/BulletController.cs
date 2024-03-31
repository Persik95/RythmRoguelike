using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private Rigidbody2D rigidbodyBullet;

    void Start()
    {
        rigidbodyBullet = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        rigidbodyBullet.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}