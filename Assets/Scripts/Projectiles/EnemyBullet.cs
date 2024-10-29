using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<PlayerController>()) {
            // Bullet is destroyed.
            Destroy(gameObject, 0.01f);

            // Enemy takes damage
            collision.GetComponent<HealthController>().TakeDamage(1);

            // Prevents repeated hits on the opponent per bullet
            // Invincibility ends when the bullet exits bounding box.
            collision.GetComponent<HealthController>().InitIFrames();

        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
         
        if (collision.GetComponent<PlayerController>()) {
            collision.GetComponent<HealthController>().ExitIFrames();
        }
    }

}
