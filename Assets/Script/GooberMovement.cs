using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberMovement : MonoBehaviour
{
    CapsuleCollider2D GooberCapsuleCollider;
    Rigidbody2D GooberRigidBody;
    BoxCollider2D GooberBoxCollider;
    [SerializeField] float GooberSpeed = 1.0f;
    void Start()
    {
        GooberRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GooberRigidBody.velocity = new Vector2(GooberSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GooberSpeed = -GooberSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(GooberRigidBody.velocity.x)), 1f);
    }
}
