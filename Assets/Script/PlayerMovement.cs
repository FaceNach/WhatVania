using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 MoveInput;
    Rigidbody2D PlayerRigidBody2D;
    [SerializeField] float SpeedPlayer = 10.0f;
    [SerializeField] float Gravity = 10.0f;
    [SerializeField] float PlayerJumpSpeed = 7.0f;
    [SerializeField] float ClimbSpeed = 5.0f;
    [SerializeField] float GravityScaleStart = 1.0f;
    [SerializeField] Vector2 DeathKick = new Vector2(10.0f,10.0f);
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform BulletGun;
    Animator MyAnimator;
    CapsuleCollider2D PlayerCapsuleCollider;
    BoxCollider2D PlayerBoxCollider;
    bool IsAlive = true;

    
    void Start()
    {
       PlayerRigidBody2D = GetComponent<Rigidbody2D>();
       MyAnimator = GetComponent<Animator>();
       PlayerCapsuleCollider = GetComponent<CapsuleCollider2D>();
       PlayerBoxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (!IsAlive)
        {
            return;

        }

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!IsAlive)
        {
            return;

        }
        MoveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!IsAlive)
        {
            return;

        }

        if (!PlayerBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            PlayerRigidBody2D.velocity += new Vector2(0f, PlayerJumpSpeed);
        }
    }

    void Run()
    {
        Vector2 PlayerVelocity = new Vector2(MoveInput.x * SpeedPlayer, PlayerRigidBody2D.velocity.y);
        PlayerRigidBody2D.velocity = PlayerVelocity;

        bool PlayerHasHorizontalSpeed = Mathf.Abs(PlayerRigidBody2D.velocity.x) > Mathf.Epsilon; 
        MyAnimator.SetBool("isRunning", PlayerHasHorizontalSpeed); 
   
    }

    void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(PlayerRigidBody2D.velocity.x) > Mathf.Epsilon ;

        if(PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(PlayerRigidBody2D.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if(!PlayerBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            PlayerRigidBody2D.gravityScale = GravityScaleStart;
            MyAnimator.SetBool("isClimbing", false);
            return;
        }
        
        Vector2 ClimbVelocity = new Vector2(PlayerRigidBody2D.velocity.x, MoveInput.y * ClimbSpeed);
        PlayerRigidBody2D.velocity = ClimbVelocity;

        PlayerRigidBody2D.gravityScale = 0;

        bool PlayerHasVerticalSpeed = Mathf.Abs(PlayerRigidBody2D.velocity.y) > Mathf.Epsilon;
        MyAnimator.SetBool("isClimbing", PlayerHasVerticalSpeed);
    }

    void Die()
    {
        if (PlayerCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            IsAlive = false;
            MyAnimator.SetTrigger("Dying");
            PlayerRigidBody2D.velocity = DeathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue value)
    {
        if(!IsAlive) { return; }
        Instantiate(Bullet, BulletGun.position, transform.rotation);
    }
    
}
