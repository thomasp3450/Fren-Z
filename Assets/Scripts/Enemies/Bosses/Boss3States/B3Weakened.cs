using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3Weakened : State
{
  protected B3FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected Animator animator;
  int currentPosition;
  IEnumerator wait(){ 
    yield return new WaitForSeconds(2);
    hasWaited = true; 
  }

   public override void Enter(){
    _stateMachine = GetComponent<B3FSM>(); 
    healthController = GetComponent<HealthController>();
    playerAwarenessController = GetComponent<PlayerAwarenessController>();
    animator = GetComponent<Animator>();
    animator.Play("Idle");
    StartCoroutine(wait());
   }

   public override void Exit(){
   }

   public override void Tick(){
    if(hasWaited){
        _stateMachine.ChangeState<B3Walk>();
    }
   } 
 
}
