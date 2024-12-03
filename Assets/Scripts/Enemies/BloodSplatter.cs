using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        StartCoroutine(Bleeding());
        bloodObject.SetActive(true);
    }
}
