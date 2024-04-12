using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D BulletRigidBody;
    float BulletSpeed = 20.0f;
    float XSpeed;
    PlayerMovement Player;

    void Start()
    {
        BulletRigidBody = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerMovement>();
        XSpeed = Player.transform.localScale.x * BulletSpeed;
    }

    void Update()
    {
        BulletRigidBody.velocity = new Vector2(XSpeed, 0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
