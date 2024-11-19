using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerEvent : MonoBehaviour
{  
   bool hasSpawned;

    public GameObject[] bossObjects;
    void Start()
    {
      hasSpawned = false;
  
      bossObjects = GameObject.FindGameObjectsWithTag("Boss");
      
        foreach (GameObject bossObject in bossObjects)
        {
            bossObject.SetActive(false);
        }
    }
   

   IEnumerator SpawnBoss(){
      hasSpawned = true;
      yield return new WaitForSeconds(.5f);
      foreach (GameObject bossObject in bossObjects)
        {
            bossObject.SetActive(true);
        }
   }

   private void OnTriggerEnter2D (Collider2D collision){
      if (collision.GetComponent<PlayerController>()) {
         if(!hasSpawned){
            StartCoroutine(SpawnBoss());
         }
       }
      
   }
}
