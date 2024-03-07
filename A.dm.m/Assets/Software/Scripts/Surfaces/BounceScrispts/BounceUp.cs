using UnityEngine;

public class BounceUp : MonoBehaviour
{
    float Bounce = 20f;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Bounce, ForceMode2D.Impulse);
            other.gameObject.GetComponent<PMovement>().jumpCount = 1;
            Debug.Log("UpCollison");
        }
    }
}
