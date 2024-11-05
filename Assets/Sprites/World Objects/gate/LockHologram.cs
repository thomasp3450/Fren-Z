using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockHologram : MonoBehaviour
{
    [SerializeField] GameObject GateAttached;

    GateLogic gateLogic;

    Animator animator;
    BoxCollider2D collider;
    void Start()
    {
        gateLogic = GateAttached.GetComponent<GateLogic>();
        animator = GetComponent<Animator>();
        
    }

    IEnumerator Unlock(){
        animator.Play("Unlock");
        yield return new WaitForSeconds(2);
    }

    IEnumerator Deny(){
        animator.Play("LockDeny");
        yield return new WaitForSeconds(2);
        animator.Play("Locked");
    }


     void OnTriggerEnter2D (Collider2D collision){
        // if(gateLogic.isTriggered){
        //     if(gateLogic.hasAccess){
        //         StartCoroutine(Unlock());
        //     }else{
        //         StartCoroutine(Deny());
        //     }
           
        // }
     }
    
}
