using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CollectBloodBomb : MonoBehaviour {

    private bool _isUsed = false;

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<PlayerController>() && _isUsed == false) {
            collision.GetComponent<PlayerController>()._amountOfBloodBombs += 1;
            collision.GetComponent<PlayerController>().progressData.SetProgressData(collision.GetComponent<PlayerController>().GetCurrentLevel(), collision.GetComponent<PlayerController>()._amountOfBloodBombs, collision.GetComponent<PlayerController>()._amountOfSyringes);
            collision.GetComponent<PlayerController>().SaveData();
            // Debug.Log("Found blood bomb! Player has " + collision.GetComponent<PlayerController>()._amountOfBloodBombs + " blood bombs!");
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            _isUsed = true;
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<PlayerController>()) {
        }
    }
    
}
