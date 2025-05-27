using UnityEngine;

public class PMovement : MonoBehaviour
{
    
    float pHorizontal;
    public float groundCheckRadius = .35f;
    float coyoteTime = 0.2f;
    float coyoteTimeCounter;
    
    public bool isGrounded;
    bool isFacingRight = true;
    bool isCrouched;
    bool isJumped;

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
        AnimationParameters();
        TestDieVoid();

        pHorizontal = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate() {
        Movement();
    }
    

    void Movement()
    {
        rb.velocity = new Vector2(pHorizontal * speed * Time.deltaTime, rb.velocity.y);
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
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.transform.position, groundCheckRadius, groundCheckLayer);
    }

    void Jump()
    {
        if(attackScript.isSmashing == false && isCrouched == false)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !isJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f);
                isJumped = true;
                coyoteTimeCounter = 0;
            }

            if(isGrounded)
            {
                isJumped = false;
            }
            else
            {
                isJumped = true;
            }
            
        }
    }

    void Coyote()
    {
        if(isGrounded && !isJumped)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

    }

    void Crouch()
    {
        if(isGrounded)
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
        else if(isGrounded == false && isCrouched)
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
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isCrouched", isCrouched);
    }

    void TestDieVoid()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = respawnPoint.transform.position;
        }
    }

    


    private void OnCollisionEnter2D(Collision2D other) {
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
