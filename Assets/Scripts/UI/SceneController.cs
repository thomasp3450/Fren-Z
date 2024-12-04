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
        SceneManager.LoadSceneAsync(scene.buildIndex + 1);
        scene = SceneManager.GetActiveScene();
    }

    

}
