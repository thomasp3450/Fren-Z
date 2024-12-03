using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKey : MonoBehaviour
{
  
    public bool Collected;
    [SerializeField] public GameObject BossAttached;

    // Start is called before the first frame update
    void Start()
    {
        Collected = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!BossAttached.gameObject.activeInHierarchy){
            gameObject.SetActive(true);
        }
    }

    public bool isCollected(){
        return Collected;
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){ 
            Collected = true;
            gameObject.SetActive(false);
        }
    }
}
