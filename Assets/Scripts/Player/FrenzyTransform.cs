using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FrenzyTransform : MonoBehaviour {

    public void OnChangeFrenziedState (InputAction.CallbackContext context) {
        // If player fills the Frenzy gauge.
        if (!gameObject.GetComponent<PlayerPause>()._pauseMenu.activeSelf && !gameObject.GetComponent<PlayerGameOver>()._gameoverMenu.activeSelf && gameObject.GetComponent<PlayerController>()._activeTransformCooldown <= 0 && gameObject.GetComponent<PlayerController>()._isFrenzied && gameObject.GetComponent<PlayerController>().GetFrenzyMeter() >= (float)(gameObject.GetComponent<PlayerController>().GetFrenzyMeterMax() - 27)) { 
            gameObject.GetComponent<HealthController>().AddHealth(1);
            gameObject.GetComponent<PlayerController>().ExitFrenzyMode();
            gameObject.GetComponent<PlayerController>()._activeTransformCooldown += gameObject.GetComponent<PlayerController>()._transformCooldown;
        }
    }

}