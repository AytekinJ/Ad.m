using UnityEngine;

public class PMovement : MonoBehaviour
{
    
    float pHorizontal;
    public bool groundCheck;
    bool isFacingRight = true;
    public float groundCheckRadius = .35f;
    float jumpTimeCounter = 1;
    public int jumpCount;
    bool canDoubleJump;
    bool jumpStarts;
    bool isCrouched;

    float coyoteTime = 0.2f;
    float coyoteTimeCounter;

    bool jumpBuffer;
    float jumpBufferAngle = 1f;
    public GameObject jumpBufferPos;

    public bool bouncing;
    public float bounceCounter;

    public GameObject groundCheckPosition;
    public GameObject respawnPoint;
    public LayerMask groundCheckLayer;

    

    float speed = 300f;
    PAttack attackScript;
    Rigidbody2D rb;
    Animator animator;

    

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackScript = GetComponent<PAttack>();
    }

    void Update()
    {
        CheckSurfaceForMovement();
        Coyote();
        Jump();
        Crouch();
        Flip();
        WallBounce();
        AnimationParameters();
        TestDieVoid();

        pHorizontal = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate() {
        Movement();
    }
    

    void Movement()
    {
        if(bouncing == false)
        {
            rb.velocity = new Vector2(pHorizontal * speed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            bounceCounter -= Time.deltaTime;
            if(bounceCounter < 0)
            {
                bouncing = false;
            }
        }

        
        
    }
    void Flip()
    {
        if(isFacingRight && pHorizontal < 0f || !isFacingRight && pHorizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void CheckSurfaceForMovement()
    {
        groundCheck = Physics2D.OverlapCircle(groundCheckPosition.transform.position, groundCheckRadius, groundCheckLayer);
        jumpBuffer = Physics2D.OverlapCapsule(jumpBufferPos.transform.position, new Vector2(0.5f ,1), CapsuleDirection2D.Vertical, jumpBufferAngle, groundCheckLayer);
    }
    void Jump()
    {
        if(attackScript.isSmashing == false && isCrouched == false)
        {
            if(Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f)
            {
                jumpStarts = true;
                jumpTimeCounter = 0.2625f;
                jumpCount = 1;
                //rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
            else if(jumpStarts == true && Input.GetKey(KeyCode.Space))
            {
                if(jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 6f);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else if(jumpTimeCounter < 0)
                {
                    jumpStarts = false;
                }
            }

            
            if(Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.S) && coyoteTimeCounter <= 0 && jumpCount == 1)
            {
                jumpStarts = true;
                jumpTimeCounter = 0.2625f;
                canDoubleJump = true;
                animator.SetTrigger("JumpTrigger");
                jumpCount = 0;
                //rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
            else if(Input.GetKey(KeyCode.Space) && canDoubleJump && jumpStarts == true)
            {
                if(jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 6f);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else if(jumpTimeCounter < 0)
                {
                    jumpStarts = false;
                    canDoubleJump = false;
                }
            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                jumpStarts = false;
                jumpTimeCounter = 0;
                canDoubleJump = false;
                coyoteTimeCounter = 0;
            }
        }

        #region Fly Codes (i did it by accident)
        // if (groundCheck == true && attackScript.isSmashing == false && isCrouched == false)
        // {
        //     if(Input.GetKeyDown(KeyCode.Space))
        //     {
        //         jumpStarts = true;
        //         reachedMaxHigh = false;
        //         doubleJumped = false;
                
        //     }

        // }
        // else if(groundCheck == false && attackScript.isSmashing == false && isCrouched == false && doubleJumped == false)
        // {
        //     if(Input.GetKeyDown(KeyCode.Space))
        //     {
        //         jumpStarts = true;
        //         reachedMaxHigh = false;
        //         doubleJumped = true;
        //         animator.SetTrigger("JumpTrigger");
        //     }
            
        // }
        
        // if(Input.GetKey(KeyCode.Space) && reachedMaxHigh == false && jumpStarts)
        // {
        //     jumpStage += Time.deltaTime;
        //     if(jumpStage < 1)
        //     {
        //         rb.velocity += new Vector2(0, 50f * Time.deltaTime);
        //     }
        //     else
        //     {
        //         rb.velocity += new Vector2(0, 200f * Time.deltaTime);
        //     }

        //     if(rb.velocity.y > 20)
        //     {
        //         reachedMaxHigh = true;
        //     }
        // }

        // if(Input.GetKeyUp(KeyCode.Space))
        // {
        //     jumpStarts = false;
        // }
        #endregion
            

        #region Old Jump Codes
        // if (groundCheck == true && attackScript.isSmashing == false && isCrouched == false)
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, jumpPover);
        //         doubleJumped = false;
        //         animator.SetTrigger("JumpTrigger");
        //     }

        // }
        // else if(groundCheck == false && doubleJumped == false && !Input.GetKey(KeyCode.S) && attackScript.isSmashing == false)
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, jumpPover);
        //         doubleJumped = true;
        //         animator.SetTrigger("JumpTrigger");
        //         Debug.Log("Double Jumped");
        //     }
        // }
        #endregion


    }

    void Crouch()
    {
        if(groundCheck)
        {
            if(Input.GetKeyDown(KeyCode.S) && attackScript.isSmashing == false)
            {
                Debug.Log("Crouched");
                isCrouched = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
                GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.4f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(1.2f, 0.6f);
            }
            else if(Input.GetKeyUp(KeyCode.S) && attackScript.isSmashing == false)
            {
                Debug.Log("Standing Up");
                isCrouched = false;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
                GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.7f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(0.85f, 1f);
            }
            
        }
        else if(groundCheck == false && isCrouched)
        {
            isCrouched = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.7f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.85f, 1f);
        }
        
    }

    void AnimationParameters()
    {
        animator.SetFloat("Horizontal", Mathf.Abs(pHorizontal));
        animator.SetFloat("VerticalVel", rb.velocity.y);
        animator.SetBool("isGrounded", groundCheck);
        animator.SetBool("isCrouched", isCrouched);
    }

    

    void TestDieVoid()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = respawnPoint.transform.position;
        }
    }

    void Coyote()
    {
        if(jumpBuffer)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

    }

    void WallBounce()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            rb.velocity = new Vector2(1f * 10f, 1f * 10f);
            bounceCounter = 1;
            bouncing = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("WallRightForce"))
        {
            
            rb.velocity = new Vector2(1f * 20f, 1f * 20f);
            bounceCounter = 0.5f;
            bouncing = true;
        }
        else if(other.gameObject.CompareTag("WallLeftForce"))
        {
            
            rb.velocity = new Vector2(-1f * 20f, 1f * 20f);
            bounceCounter = 0.5f;
            bouncing = true;
        }

        if(other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
            
        }
    }

    private void OnCollisionExit2D(Collision2D other) {

        if(other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.transform.position, groundCheckRadius);
        
    }

    

   

}
