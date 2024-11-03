using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideState : State{ //state that initializes the boss and its components and immediately transitions to its Idle State for decisionmaking 
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;

  public override void Enter(){
    _stateMachine = GetComponent<StateMachine>(); 
    healthController = GetComponent<HealthController>();
    playerAwarenessController = GetComponent<PlayerAwarenessController>();
  }
  public override void Exit(){}
  public override void Tick(){
    _stateMachine.ChangeState<B1Idle>();
  }
}
