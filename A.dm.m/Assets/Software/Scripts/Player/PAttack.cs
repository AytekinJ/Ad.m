using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class PAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.2f;
    public bool isSmashing;
    float attackCoolDown;
    public bool smashed;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    PMovement movement;
    Animator animator;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PMovement>();
    }

    void Update()
    {
        Attack();
        
    }

    void Attack()
    {
        if(Input.GetKey(KeyCode.S) && movement.isGrounded == false && attackCoolDown < 0.01)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Smashed");
                isSmashing = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(0, -10f);
                animator.SetTrigger("SmashTrigger");
                attackCoolDown = 2.5f;
                smashed = false;
                
            }
        }
        else if(attackCoolDown > 0)
        {
            attackCoolDown-= Time.deltaTime;
        }

        if(isSmashing)
        {
            StartCoroutine(AttackEnd());
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer); //Yere vurduğunda yere vurdum yazsın diye ground layer koydum bunu enemy layer ile değiştir
            foreach(Collider2D enemy in hitEnemies)
            {
                if(enemy.gameObject.CompareTag("Enemy") && smashed == false)
                {
                    Destroy(enemy.gameObject);
                    smashed = true;
                    Debug.Log("Hit " + enemy.name);
                }
                
            }
        }

        if(movement.isGrounded && isSmashing)
        {
            smashed = true;
        }

        animator.SetBool("isSmashed", smashed);
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
        
    }

    

    IEnumerator AttackEnd()
    {
        if(movement.isGrounded)
        {
            yield return new WaitForSeconds(0.45f);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isSmashing = false;
            smashed = false;
        }
        
    }
    
}
