using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{

    public bool Collected;
    [SerializeField] public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
