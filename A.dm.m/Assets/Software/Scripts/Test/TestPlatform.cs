using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlatform : MonoBehaviour
{
    bool reachedB;
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if(transform.position.x < 40 && reachedB == false)
        {
            rb.velocity = new Vector2(2f, 0);
            
        }
        else if(transform.position.x > 36)
        {
            reachedB = true;
            rb.velocity = new Vector2(-2f, 0);

        }
        else
        {
            reachedB = false;
        }
    }

    
}
