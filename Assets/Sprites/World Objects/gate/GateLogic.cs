using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour
{
   
    [SerializeField] public GameObject Key;
    Keycard key;
    private Animator animator;

    public bool hasAccess;

    public bool isColliderEnabled;

    IEnumerator wait(){ 
        yield return new WaitForSeconds(5); //stay open 5 seconds
    }
    
    void Start()
    {   
        key = Key.gameObject.GetComponent<Keycard>();
        hasAccess = false;
        isColliderEnabled = true;
        animator = GetComponent<Animator>();
        
    }

    void Update(){
        if(key.isCollected()){
            hasAccess = true;
        }

        if(isColliderEnabled = false){
            GetComponent<Collider2D>().enabled = false;
        }else{
            GetComponent<Collider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){ 
        
            if(hasAccess){
                isColliderEnabled = false;
                StartCoroutine(AllowAccess());
                isColliderEnabled = true;
            }else{
                StartCoroutine(DenyAccess());
            }
        }
    }

    IEnumerator AllowAccess(){
        animator.Play("GateOpen");
        yield return new WaitForSeconds(5);
        animator.Play("GateClosed");
    }

     IEnumerator DenyAccess(){
        animator.Play("GateDeny");
        yield return new WaitForSeconds(2);
        animator.Play("GateClosed");
    }


}
