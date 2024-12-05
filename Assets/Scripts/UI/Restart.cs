using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Respawn(){
        if (SceneManager.GetActiveScene().name == "Level 1") SceneManager.LoadSceneAsync("Level 1");
        if (SceneManager.GetActiveScene().name == "Level 2") SceneManager.LoadSceneAsync("Level 2");
        if (SceneManager.GetActiveScene().name == "Level 3") SceneManager.LoadSceneAsync("Level 3");
        if (SceneManager.GetActiveScene().name == "Level1Boss") SceneManager.LoadSceneAsync("Level1Boss");
        if (SceneManager.GetActiveScene().name == "Level2Boss") SceneManager.LoadSceneAsync("Level2Boss");
        if (SceneManager.GetActiveScene().name == "Level3Boss") SceneManager.LoadSceneAsync("Level3Boss");
    }
}
