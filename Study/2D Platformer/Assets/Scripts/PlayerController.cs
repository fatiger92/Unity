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
    static readonly int ANIM_PARAM_DOUBLEJUMP = Animator.StringToHash("doDoubleJump");
    static readonly int ANIM_PARAM_KNOCKINGBACK = Animator.StringToHash("isKnockingBack");
    
    public float knockBackLength, knockBackSpeed;
    float knockBackCounter;

    void Update()
    {
        if (Time.timeScale <= 0f)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        
        //theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        if (knockBackCounter <= 0)
        {

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
                        anim.SetTrigger(ANIM_PARAM_DOUBLEJUMP);
                    }
                }
            }

            if (theRB.velocity.x > 0)
                transform.localScale = Vector3.one;

            if (theRB.velocity.x < 0)
                transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            theRB.velocity = new Vector2( knockBackSpeed * -transform.localScale.x, theRB.velocity.y);
        }
            // handle animation
            anim.SetFloat(ANIM_PARAM_SPEED, Mathf.Abs(theRB.velocity.x));
            anim.SetBool(ANIM_PARAM_GROUNDED, isGrounded);
            anim.SetFloat(ANIM_PARAM_YSPEED, theRB.velocity.y);
        
    }

    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        AudioManager.instance.PlaySFXPitched(14);
    }

    public void KnockBack()
    {
        theRB.velocity = new Vector2(0f, jumpForce * .5f);
        anim.SetTrigger(ANIM_PARAM_KNOCKINGBACK);

        knockBackCounter = knockBackLength;
    }

    public void BouncePlayer(float bounceAmount)
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceAmount);
        
        canDoubleJump = true;
        
        anim.SetBool(ANIM_PARAM_GROUNDED, true);
    }
}