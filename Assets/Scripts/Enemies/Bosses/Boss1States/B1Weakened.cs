using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1Weakened : State
{
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;

  IEnumerator wait(){ //wait for a 3-5 seconds in idle before transitioning to the idle
    yield return new WaitForSeconds(Random.Range(3f, 5f)); 
  }

  private void Awake(){
    _stateMachine = GetComponent<StateMachine>(); 
    healthController = GetComponent<HealthController>();
    playerAwarenessController = GetComponent<PlayerAwarenessController>();

  }

   public override void Enter(){
    StartCoroutine(wait());
   }

   public override void Exit(){
    
   }

   public override void Tick(){
     _stateMachine.ChangeState<B1Idle>();
   } 
}
