using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Idle : State
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
      
   }

   

   public override void Exit(){
    
    if(playerAwarenessController.AwareOfPlayer){
          
        } else{
          
        }
   }

   public override void Tick(){

    if(hasWaited){
        if(playerAwarenessController.AwareOfPlayer){
            _stateMachine.ChangeState<B2Spin>();
        } else{
            _stateMachine.ChangeState<B2EnergyPistol>();
        }   
    } 
   }

}
