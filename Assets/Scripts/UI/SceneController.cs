using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public ProgressData progressData;
    [SerializeField] Animator transitionAnimation;
    Scene scene;

    GameObject player;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

     private void Start(){
        progressData = ProgressData.Instance;
        scene = SceneManager.GetActiveScene();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);
     }

    public void BackToMenu(){
        SceneManager.LoadSceneAsync(0);
        transitionAnimation.Play("FadeOut");
    }

    public void NextLevel(){
        if (SceneManager.GetActiveScene().name == "Level 1") {
            player.GetComponent<PlayerController>().progressData.SetProgressData(1, player.GetComponent<PlayerController>()._amountOfBloodBombs, player.GetComponent<PlayerController>()._amountOfSyringes);
        }
        if (SceneManager.GetActiveScene().name == "Level 2") player.GetComponent<PlayerController>().progressData.SetProgressData(2, player.GetComponent<PlayerController>()._amountOfBloodBombs, player.GetComponent<PlayerController>()._amountOfSyringes);
        if (SceneManager.GetActiveScene().name == "Level 3") player.GetComponent<PlayerController>().progressData.SetProgressData(3, player.GetComponent<PlayerController>()._amountOfBloodBombs, player.GetComponent<PlayerController>()._amountOfSyringes);
        SceneManager.LoadSceneAsync(scene.buildIndex + 1);
        transitionAnimation.Play("FadeOut");
        scene = SceneManager.GetActiveScene();
    }

    

}
