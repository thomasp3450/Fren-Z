using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TendrilAttack : MonoBehaviour
{
private void OnTriggerEnter2D (Collider2D collision) {

    if (collision.GetComponent<PlayerController>()) {
        gameObject.SetActive(false);
        if (collision.GetComponent<PlayerController>()._isFrenzied) {
            if (!collision.GetComponent<PlayerController>()._gaugeInvincible) {
                collision.GetComponent<SpriteFlash>().StartFlash((float)0.12, new Color((float)255,(float)0.0,(float)0.0), 1);
                collision.GetComponent<PlayerController>().ChangeFrenzyGauge(-7);
            }
        } else {
            if (collision.GetComponent<HealthController>()._isInvincible) {
                collision.GetComponent<SpriteFlash>().StartFlash((float)0.12, new Color((float)255,(float)0.0,(float)0.0), 1);
                collision.GetComponent<HealthController>().TakeDamage(1);
                collision.GetComponent<HealthController>().InitIFrames();
            }
        }
    }
 }
private void OnTriggerExit2D (Collider2D collision) {
    if (collision.GetComponent<EnemyMovement>()) {
        collision.GetComponent<HealthController>().ExitIFrames();
    }
}
}
