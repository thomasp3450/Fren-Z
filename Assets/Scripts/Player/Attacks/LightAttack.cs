using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : MonoBehaviour {

    bool isColliding;
    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            // gameObject.SetActive(false);
            isColliding = true;
            Debug.Log("Light attack landed. Enemy HP: " + collision.GetComponent<HealthController>().getHealth());
            collision.GetComponent<HealthController>().TakeDamage(5);
            collision.GetComponent<HealthController>().InitIFrames();
            Destroy(gameObject, 0.01f);
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<EnemyMovement>()) {
            isColliding = false;
            collision.GetComponent<HealthController>().ExitIFrames();
        }
    }
}
