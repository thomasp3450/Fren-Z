using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            // Bullet is destroyed.
            Destroy(gameObject, 0.01f);

            // Enemy takes damage
            collision.GetComponent<HealthController>().TakeDamage(1);

            // Prevents repeated hits on the opponent per bullet
            // Invincibility ends when the bullet exits bounding box.
            collision.GetComponent<HealthController>().InitIFrames();

            // I have no idea if this return does anything. I'll make sure to check when I get home.
            return;

        }

    }

    private void OnTriggerExit2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            collision.GetComponent<HealthController>().ExitIFrames();
        }
    }

}
