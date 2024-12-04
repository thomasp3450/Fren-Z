using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour
{
   
    [SerializeField] public GameObject Key;
    
    Keycard key;
    BossKey bossKey;
    private Animator animator;

    public bool hasAccess;

    public bool isColliderEnabled;

    public bool isColliderActive(){
        return isColliderEnabled;
    }


    IEnumerator wait(){ 
        yield return new WaitForSeconds(5); //stay open 5 seconds
    }
    
    void Start()
    {   
        if(Key.gameObject.GetComponent<Keycard>() != null){
            key = Key.gameObject.GetComponent<Keycard>();
        }
        if(Key.gameObject.GetComponent<BossKey>() != null){
            bossKey = Key.gameObject.GetComponent<BossKey>();
        }
        
        hasAccess = false;

        isColliderEnabled = true;
        animator = GetComponent<Animator>();
        
    }

    void Update(){
        if(Key.gameObject.GetComponent<Keycard>() != null){
            if(key.isCollected()){
                hasAccess = true;
            }
        }
        if(Key.gameObject.GetComponent<BossKey>() != null){
             if(bossKey.isCollected()){
                hasAccess = true;
            }
        }

        if (isColliderEnabled == false){
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
                
                
            }else{
                StartCoroutine(DenyAccess());
            }
        }
    }

    IEnumerator AllowAccess(){
        
        animator.Play("GateOpen");
        AudioManager.Instance.PlaySFX("DoorWoosh");
        yield return new WaitForSeconds(1);
        isColliderEnabled = false;
    }

   
     IEnumerator DenyAccess(){
        animator.Play("GateDeny");
        yield return new WaitForSeconds(2);
        animator.Play("GateClosed");
    }


}
