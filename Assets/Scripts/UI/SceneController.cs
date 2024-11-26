using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] Animator transitionAnimation;
    Scene scene;

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
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);
     }

    public void NextLevel(){
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel(){
        transitionAnimation.SetTrigger("Start");
        SceneManager.LoadSceneAsync(scene.buildIndex + 1);
        yield return new WaitForSeconds(2);
        transitionAnimation.SetTrigger("End");
    }


}