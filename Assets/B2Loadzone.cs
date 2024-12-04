using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Loadzone : MonoBehaviour
{
   
    private BoxCollider2D collider2D;
    
    private void Awake(){
        collider2D = GetComponent<BoxCollider2D>();
        
    }
    private void Start(){
        gameObject.SetActive(false);
    }

    IEnumerator LoadNextLevel(){
        yield return new WaitForSeconds(1);
        SceneController.Instance.NextLevel();
    }
   private void OnTriggerEnter2D(Collider2D collision){
    if(collision.CompareTag("Player")){
        StartCoroutine(LoadNextLevel());
    }
   }
}
