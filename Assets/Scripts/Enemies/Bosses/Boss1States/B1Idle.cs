using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1Idle : State
{
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;

  
  IEnumerator wait(){ //wait for a 3-5 seconds in idle before transitioning to the attacks
    yield return new WaitForSeconds(Random.Range(3, 5)); 
  }
  
   public override void Enter(){ 
      _stateMachine = GetComponent<StateMachine>(); 
      healthController = GetComponent<HealthController>();
      playerAwarenessController = GetComponent<PlayerAwarenessController>();
   }

   

   public override void Exit(){
    
   }

   public override void Tick(){
   StartCoroutine(wait()); 
   if(playerAwarenessController.AwareOfPlayer){
       _stateMachine.ChangeState<B1Slam>();
    } else{
        _stateMachine.ChangeState<B1Volley>();
    }
   StartCoroutine(wait()); 
   } 
   
}
