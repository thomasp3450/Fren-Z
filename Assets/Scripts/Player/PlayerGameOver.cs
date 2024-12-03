using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGameOver : MonoBehaviour {
    [SerializeField] public GameObject _gameoverMenu;

    void Start() {
        if (_gameoverMenu == null) _gameoverMenu = GameObject.Find("GameOverMenu2");
    }

    private void FixedUpdate() {
    }

    // Start is called before the first frame update
    public void OnGameOver (InputAction.CallbackContext context) {
        // If player dies
        _gameoverMenu.SetActive(true);
    }
}