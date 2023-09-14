using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    [SerializeField] private AudioSource jump;

    [SerializeField] private LayerMask jumpableGround;

    public float speed;
    public float jumpForce;
    private float dirX = 0f;

    private enum MovementState { idle, running, jumping, falling };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateAnimationUpdate();

        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);


        if (Input.GetKeyDown("space") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump.Play();
        }

        
    
    }

    void UpdateAnimationUpdate()
    {
        MovementState state;
        if (dirX > 0f)
        {
            sprite.flipX = false;
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            
            state = MovementState.jumping;
        } 
        else if (rb.velocity.y < -.1f)
        {
            
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, jumpableGround);
    }
}
