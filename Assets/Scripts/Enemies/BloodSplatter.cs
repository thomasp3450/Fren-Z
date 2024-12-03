using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BloodSplatter : MonoBehaviour {
    [SerializeField] public GameObject bloodObject;

    void OnEnable() {
    }

    public IEnumerator Bleeding() {
        // Coroutine for the deactivation of dead enemies.
        yield return new WaitForSeconds(.3f);
        gameObject.SetActive(false);
    }

    public void OnExplode() {
        try {
            StartCoroutine(Bleeding());
        } catch (Exception e) {
        }
        bloodObject.SetActive(true);
    }
}
