using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeAoE : MonoBehaviour
{ //Prefab script for syringe AoE goop
    
    [SerializeField] float projectilespeed;

    private void Start(){
       
    }

    private void Update(){

    }

    private void OnTriggerEnter2D (Collider2D collision) {

    if (collision.GetComponent<PlayerController>()) {
        gameObject.SetActive(false);
        if (collision.GetComponent<PlayerController>()._isFrenzied) {
            if (!collision.GetComponent<PlayerController>()._gaugeInvincible) {
                collision.GetComponent<SpriteFlash>().StartFlash(1, new Color((float)255,(float)0.0,(float)0.0), 1);
                collision.GetComponent<PlayerController>().ChangeFrenzyGauge(-5);
            }
        } else {
            if (collision.GetComponent<HealthController>()._isInvincible) {
                collision.GetComponent<SpriteFlash>().StartFlash(1, new Color((float)255,(float)0.0,(float)0.0), 1);
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
