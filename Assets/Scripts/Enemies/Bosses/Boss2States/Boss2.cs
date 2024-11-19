using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : State
{  
 //state that initializes the boss and its components and immediately transitions to its Idle State for decisionmaking 
  protected B2FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;

  public override void Enter(){
    _stateMachine = GetComponent<B2FSM>(); 
    healthController = GetComponent<HealthController>();
    playerAwarenessController = GetComponent<PlayerAwarenessController>();
  }
  public override void Exit(){}
  public override void Tick(){
    _stateMachine.ChangeState<B2Idle>();
  }
}



