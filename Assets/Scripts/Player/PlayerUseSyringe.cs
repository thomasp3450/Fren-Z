using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerUseSyringe : MonoBehaviour {

    public void OnSyringe (InputAction.CallbackContext context) {
        if (!gameObject.GetComponent<PlayerPause>()._pauseMenu.activeSelf && gameObject.GetComponent<PlayerController>()._amountOfSyringes > 0 && gameObject.GetComponent<PlayerController>()._isFrenzied) {
            gameObject.GetComponent<PlayerController>()._amountOfSyringes--;
            gameObject.GetComponent<HealthController>().AddHealth(1);
            gameObject.GetComponent<PlayerController>().ExitFrenzyMode();
        }
    }
}
