using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyHitFlash>()) {
            // Debug.Log("Heavy attack landed against " + collision.gameObject.name);
            collision.GetComponent<HealthController>().TakeDamage(10);
        }
    }

}
