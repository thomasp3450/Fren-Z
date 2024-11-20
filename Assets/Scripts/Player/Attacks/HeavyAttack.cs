using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyHitFlash>()) {
            // Debug.Log("Heavy attack landed against " + collision.gameObject.name);
            // gameObject.SetActive(false);
            collision.GetComponent<HealthController>().TakeDamage(10);
            collision.GetComponent<HealthController>().InitIFrames();
        } else if (!collision.GetComponent<PlayerController>()) {
            // Debug.Log("Frenzy mode entered.");
            // Debug.Log("Heavy attack landed against incorrect " + collision.gameObject.name);
        } 
    }

    private void OnTriggerExit2D (Collider2D collision) {

        if (collision.GetComponent<EnemyHitFlash>()) {
            collision.GetComponent<HealthController>().ExitIFrames();
        }
    }
}
