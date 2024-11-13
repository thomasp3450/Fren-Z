using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<PlayerController>()) {
            // Bullet is destroyed.
            Destroy(gameObject, 0.01f);

            if (collision.GetComponent<PlayerController>()._isFrenzied) {
                if (!collision.GetComponent<PlayerController>()._gaugeInvincible) collision.GetComponent<PlayerController>().ChangeFrenzyGauge(-2);
                collision.GetComponent<SpriteFlash>().StartFlash(1, new Color((float)255,(float)0.0,(float)0.0), 1);
            } else {
                // Enemy takes damage
                collision.GetComponent<HealthController>().TakeDamage(1);
                collision.GetComponent<SpriteFlash>().StartFlash(1, new Color((float)255,(float)0.0,(float)0.0), 1);
            }

            // Prevents repeated hits on the opponent per bullet
            // Invincibility ends when the bullet exits bounding box.
            collision.GetComponent<HealthController>().InitIFrames();

        }
        
        if(collision.gameObject.tag == "Walls"){ //prevent bullet wall passthrough
            Destroy(gameObject, 0.01f);
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
         
        if (collision.GetComponent<PlayerController>()) {
            collision.GetComponent<HealthController>().ExitIFrames();
        }
    }

}
