using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public float runSpeed;
    float activeSpeed;

    bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    bool canDoubleJump;

    public Animator anim;
    static readonly int ANIM_PARAM_SPEED = Animator.StringToHash("speed");
    static readonly int ANIM_PARAM_GROUNDED = Animator.StringToHash("isGrounded");
    static readonly int ANIM_PARAM_YSPEED = Animator.StringToHash("ySpeed");

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        
        //theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);
     
        activeSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;
        }

        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                //theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                Jump();
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    //theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    Jump();
                    canDoubleJump = false;
                }
            }
        }

        transform.localScale = theRB.velocity.x switch
        {
            > 0 => Vector3.one,
            < 0 => new Vector3(-1f, 1f, 1f),
            _ => transform.localScale
        };

        // handle animation
        anim.SetFloat(ANIM_PARAM_SPEED,Mathf.Abs(theRB.velocity.x));
        anim.SetBool(ANIM_PARAM_GROUNDED, isGrounded);
        anim.SetFloat(ANIM_PARAM_YSPEED, theRB.velocity.y);
    }

    void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }
}