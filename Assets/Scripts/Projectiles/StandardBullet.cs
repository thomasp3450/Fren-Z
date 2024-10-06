using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            Destroy(gameObject, 0.01f);
            collision.GetComponent<HealthController>().TakeDamage(1);

            if(collision.GetComponent<HealthController>().getHealth() <= 0) {
                Destroy(collision.gameObject);
            }
        }

    }

}
