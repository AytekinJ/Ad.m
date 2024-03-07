using UnityEngine;

public class BounceRight : MonoBehaviour
{
    float Bounce = 20f;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Bounce);
            other.gameObject.GetComponent<PMovement>().jumpCount = 1;
            Debug.Log("RightCollison");
        }
    }
}
