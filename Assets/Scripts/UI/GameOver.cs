using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void OnDisable() {
        Time.timeScale = 1;
    }

    void OnEnable() {
        Time.timeScale = 0;
    }
}