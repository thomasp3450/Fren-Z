using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : MonoBehaviour {
    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            gameObject.SetActive(false);
            Debug.Log("Light attack landed.");
            collision.GetComponent<HealthController>().TakeDamage(1);
            collision.GetComponent<HealthController>().InitIFrames();
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<EnemyMovement>()) {
            collision.GetComponent<HealthController>().ExitIFrames();
        }
    }
}
