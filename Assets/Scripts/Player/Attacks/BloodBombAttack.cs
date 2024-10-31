using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBombAttack : MonoBehaviour {

    public bool _hasDetonated = false;
    private GameObject _detonatedBloodBombPrefab;

    void Update() {
        // if (_hasDetonated) 
    }

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            _hasDetonated = true;
            Destroy(gameObject, 0.01f);
            // collision.GetComponent<HealthController>().TakeDamage(5);
            // collision.GetComponent<HealthController>().InitIFrames();
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<EnemyMovement>()) {
            // collision.GetComponent<HealthController>().ExitIFrames();
        }
    }

    void OnDestroy() {

        GameObject bloodBomb = Instantiate(_detonatedBloodBombPrefab, gameObject.transform.position, transform.rotation);
        Destroy(bloodBomb, 0.01f);
        
    }
}
