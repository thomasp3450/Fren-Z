using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FrenzyTransform : MonoBehaviour {

    public void OnChangeFrenziedState (InputAction.CallbackContext context) {
        // If player fills the Frenzy gauge.
        if (gameObject.GetComponent<PlayerController>()._isFrenzied && gameObject.GetComponent<PlayerController>().GetFrenzyMeter() >= gameObject.GetComponent<PlayerController>().GetFrenzyMeterMax()) { 
            gameObject.GetComponent<HealthController>().AddHealth(1);
            gameObject.GetComponent<PlayerController>().ExitFrenzyMode();
        }
    }

}