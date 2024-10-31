using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBloodBomb : MonoBehaviour {
    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<PlayerController>()) {
            gameObject.SetActive(false);
            collision.GetComponent<PlayerController>()._amountOfBloodBombs++;
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<PlayerController>()) {
        }
    }
}
