using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1Idle : State
{
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected Animator animator;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  
  IEnumerator wait(){ //wait for a 3 seconds in idle before transitioning to the attacks
    yield return new WaitForSeconds(3); 
    hasWaited = true;
  }
  
   public override void Enter(){ 
      hasWaited = false;
      _stateMachine = GetComponent<StateMachine>(); 
      playerAwarenessController = GetComponent<PlayerAwarenessController>();
      StartCoroutine(wait());
      animator = GetComponent<Animator>();
      animator.SetBool("IsIdle", true);
   }

   

   public override void Exit(){
    animator.SetBool("IsIdle", false);
    if(playerAwarenessController.AwareOfPlayer){
          animator.SetBool("IsSlamming", true);  
        } else{
          animator.SetBool("IsVolley", true); 
        }
   }

   public override void Tick(){

    if(hasWaited){
        if(playerAwarenessController.AwareOfPlayer){
            _stateMachine.ChangeState<B1Slam>();
        } else{
            _stateMachine.ChangeState<B1Volley>();
        }   
    } 
   }
}
