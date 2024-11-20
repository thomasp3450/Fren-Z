using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Idle : State
{
   protected B2FSM _stateMachine; //instantiate the FSM 
  protected Animator animator;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  
  IEnumerator wait(){ 
    yield return new WaitForSeconds(1); 
    hasWaited = true;
  }
  
   public override void Enter(){ 
      hasWaited = false;
      _stateMachine = GetComponent<B2FSM>(); 
      playerAwarenessController = GetComponent<PlayerAwarenessController>();
      StartCoroutine(wait());
      animator = GetComponent<Animator>();
      
   }

   

   public override void Exit(){
    
    if(playerAwarenessController.AwareOfPlayer){
          
        } else{
          
        }
   }

   public override void Tick(){

    if(hasWaited){
       _stateMachine.ChangeState<B2Dash>();
    } 
   }

}
