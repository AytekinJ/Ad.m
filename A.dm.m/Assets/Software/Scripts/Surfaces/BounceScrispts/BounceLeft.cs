using UnityEngine;

public class BounceLeft : MonoBehaviour
{
    //float Bounce = 20f;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            // other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(100 * Bounce, 0);
            // other.gameObject.GetComponent<PMovement>().jumpCount = 1;
            // Debug.Log("LeftCollison");
        }
    }
}
