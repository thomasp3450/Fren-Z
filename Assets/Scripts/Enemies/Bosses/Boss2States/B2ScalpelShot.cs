using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2ScalpelShot : State
{
  protected B2FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected Animator animator;
  int currentPosition;
  [SerializeField] private Transform _BossOffset; 
  [SerializeField] private GameObject scalpelProjectile;
   IEnumerator wait(){ 
      yield return new WaitForSeconds(1);
      hasWaited = true; 
   }

    public override void Enter(){
       _stateMachine = GetComponent<B2FSM>(); 
        healthController = GetComponent<HealthController>();
        currentPosition = _stateMachine.GetPosition();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
    
        StartCoroutine(wait());
    }
    public override void Exit(){} 
    public override void Tick(){
        if(hasWaited){
             _stateMachine.ChangeState<B2Weakened>();
        }
    }
}
