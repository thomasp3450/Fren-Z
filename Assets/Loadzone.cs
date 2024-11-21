using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadzone : MonoBehaviour
{   
    private BoxCollider2D collider2D;
    private void Awake(){
        collider2D = GetComponent<BoxCollider2D>();
    }
   private void OnTriggerEnter2D(Collider2D collision){
    if(collision.CompareTag("Player")){
        SceneController.Instance.NextLevel();
    }
   }
}
