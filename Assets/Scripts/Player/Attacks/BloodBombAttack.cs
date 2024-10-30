using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBombAttack : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            gameObject.SetActive(false);
            collision.GetComponent<HealthController>().TakeDamage(5);
            collision.GetComponent<HealthController>().InitIFrames();
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<EnemyMovement>()) {
            collision.GetComponent<HealthController>().ExitIFrames();
        }
    }
}
