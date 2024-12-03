using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour {
 
    public void startLevel1() {
        SceneManager.LoadSceneAsync("Level 1");
    }
}
