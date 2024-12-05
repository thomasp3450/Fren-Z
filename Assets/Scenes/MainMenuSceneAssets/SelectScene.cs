using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectScene : MonoBehaviour {
    public void startIntro(){
        SceneManager.LoadSceneAsync("Intro");
    }
 
    public void startLevel1() {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void startLevel2() {
        SceneManager.LoadSceneAsync("Level 2");
    }

    public void openMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void resetProgress() {
    }

    public void enterLevel() {
    }
}
