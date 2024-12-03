using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerPause : MonoBehaviour {
    [SerializeField] public GameObject _pauseMenu;
    private float _pauseCooldown = 0;
    private float _activePauseCooldown = 0;

    private void FixedUpdate() {
        if (_activePauseCooldown > 1) _activePauseCooldown--;
        Debug.Log(_activePauseCooldown);
    }

    // Start is called before the first frame update
    public void OnPausing (InputAction.CallbackContext context) {
        // If player pauses
        if (_activePauseCooldown <= 0) { 
            if (_pauseMenu.activeSelf == true) _pauseMenu.SetActive(false);
            else if (_pauseMenu.activeSelf == false) _pauseMenu.SetActive(true);
            _activePauseCooldown += _pauseCooldown;
        }
    }
}
