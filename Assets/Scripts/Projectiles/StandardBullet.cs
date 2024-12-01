using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StandardBullet : MonoBehaviour {

[SerializeField] GameObject impactPrefab;
 private CinemachineImpulseSource impulseSource;

    private void Start(){
       AudioManager.Instance.PlaySFX("Gunshot");
       impulseSource = GetComponent<CinemachineImpulseSource>();
       ScreenShake.Instance.ShakeCamera(impulseSource, .04f);

    }
    private void OnTriggerEnter2D (Collider2D collision) {
        GameObject impact = Instantiate(impactPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(impact, 0.1f);
        if (collision.GetComponent<EnemyMovement>()) {
            // Bullet is destroyed.
            Destroy(gameObject, 0.01f);

            // Enemy takes damage
            collision.GetComponent<HealthController>().TakeDamage((float)2.5);

            // Prevents repeated hits on the opponent per bullet
            // Invincibility ends when the bullet exits bounding box.
            collision.GetComponent<HealthController>().InitIFrames();

        }

        if(collision.gameObject.tag == "Walls" || collision.gameObject.tag == "Gate"){ //prevent bullet wall passthrough
            
            Destroy(gameObject, 0.01f);
            
        }


    }

    private void OnTriggerExit2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            collision.GetComponent<HealthController>().ExitIFrames();
        }
        
    }

}
