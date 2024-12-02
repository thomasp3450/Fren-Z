using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3Walk : State
{
    protected B3FSM _stateMachine; //instantiate the FSM 
  protected Animator animator;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  
    [SerializeField] private Transform _BossOffset; 
  
    [SerializeField] public Transform pos1, pos2, pos3;
    public float bossSpeed;

    protected bool madeDecision;

  int currentPosition;
 
   public override void Enter(){ 
      
      hasWaited = false;
      madeDecision = false;
      _stateMachine = GetComponent<B3FSM>(); 
      currentPosition = _stateMachine.GetPosition();
      playerAwarenessController = GetComponent<PlayerAwarenessController>();
      animator = GetComponent<Animator>();
      //pick destination
        
        if(currentPosition == 1){
            Dashdecision(1);   
        }
        
        else if(currentPosition == 2){
            Dashdecision(2);
        }
        
        else if(currentPosition == 3){
            Dashdecision(3);
        }   
   }

   
   public override void Exit(){
    
   }

   public override void Tick(){
    if(madeDecision){
    StartCoroutine(Dash());
    StartCoroutine(wait());
    if(hasWaited){
       _stateMachine.ChangeState<B3Idle>();
          
    }     
    
    } 
   }

    IEnumerator wait(){ 
         yield return new WaitForSeconds(2);
        hasWaited = true;
    }



    IEnumerator Dash(){
        float step = bossSpeed * Time.deltaTime;
        animator.Play("Walk");
        if(currentPosition == 1){
            yield return new WaitForSeconds(1);
            transform.position = Vector2.MoveTowards(transform.position, pos1.transform.position, step);
            
        }
        
        else if(currentPosition == 2){
            yield return new WaitForSeconds(1);
            transform.position = Vector2.MoveTowards(transform.position, pos2.transform.position, step);
        }
        
        else if(currentPosition == 3){
            yield return new WaitForSeconds(1);
            transform.position = Vector2.MoveTowards(transform.position, pos3.transform.position, step);
        }


    }


  private int Dashdecision(int currentPos){ //make decision on where to jump to 
    int dashChooserNumberThing = Random.Range(1,3);
    
    if(currentPos == 1){
        if(dashChooserNumberThing == 1){
            currentPosition = 2;
            madeDecision = true;
        }
        if(dashChooserNumberThing == 2){
            currentPosition = 3;
            madeDecision = true;
        }
    }
    
    else if(currentPos == 2){
        if(dashChooserNumberThing == 1){
            currentPosition = 1;
            madeDecision = true;
        }
        if(dashChooserNumberThing == 2){
            currentPosition = 3;
            madeDecision = true;
        }
    }
    
    else if(currentPos == 3){
        if(dashChooserNumberThing == 1){
            currentPosition = 1;
            madeDecision = true;
        }
        if(dashChooserNumberThing == 2){
            currentPosition = 2;
            madeDecision = true;
        }
    }
    _stateMachine.SetPosition(currentPosition);
    return currentPosition;   
  }


  

}


