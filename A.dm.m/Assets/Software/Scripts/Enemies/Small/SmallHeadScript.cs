using UnityEngine;

public class SmallHeadScript : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    public bool MoveEnemy;
    public bool MoveAnotherWay;
    bool reachedB;
    public Transform A, B;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if(MoveEnemy == true)
        {
            animator.SetBool("isMoving", true);

            if(MoveAnotherWay)
            {
                if(transform.position.x < B.position.x && reachedB == false)
                {
                    rb.velocity = new Vector2(5f, 0);
                    sr.flipX = false;
                    
                }
                else if(transform.position.x > A.position.x)
                {
                    reachedB = true;
                    rb.velocity = new Vector2(-5f, 0);
                    sr.flipX = true;

                }
                else
                {
                    reachedB = false;
                }
            }
            else
            {
                if(transform.position.x > A.position.x && reachedB == false)
                {
                    rb.velocity = new Vector2(-5f, 0);
                    sr.flipX = true;
                    
                }
                else if(transform.position.x < B.position.x)
                {
                    reachedB = true;
                    rb.velocity = new Vector2(5f, 0);
                    sr.flipX = false;

                }
                else
                {
                    reachedB = false;
                }
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
