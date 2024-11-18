using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Hammer : State
{
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected Animator animator;
   IEnumerator wait(){ 
      yield return new WaitForSeconds(4);
      hasWaited = true; 
   }

    public override void Enter(){
        _stateMachine = GetComponent<StateMachine>(); 
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
    
        StartCoroutine(wait());
    }

    public override void Exit(){} 
    public override void Tick(){}
}
