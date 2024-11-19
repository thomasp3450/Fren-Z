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

        if(isColliderEnabled == false){
            GetComponent<BoxCollider2D>().enabled = false;
        }else{
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){ 
        
            if(hasAccess){ 
                StartCoroutine(AllowAccess());
                //StartCoroutine(Close());
                
            }else{
                StartCoroutine(DenyAccess());
            }
        }
    }

    IEnumerator AllowAccess(){
        isColliderEnabled = false;
        animator.Play("GateOpen");
        AudioManager.Instance.PlaySFX("DoorWoosh");
        yield return new WaitForSeconds(5);
    }

    IEnumerator Close(){
        yield return new WaitForSeconds(5);
        animator.Play("GateClosed");
        isColliderEnabled = true;
    
    }

     IEnumerator DenyAccess(){
        animator.Play("GateDeny");
        yield return new WaitForSeconds(2);
        animator.Play("GateClosed");
    }


}
