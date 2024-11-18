using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Spin : State
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

   public override void Exit(){
  
   }

   public override void Tick(){
    if(hasWaited){
       int attackChooserNumberThing = Random.Range(1,4); //generates 1, 2, or 3 equally since it is (MinInclusive, MaxExclusive)
       if(attackChooserNumberThing == 1){
            _stateMachine.ChangeState<B2Syringe>();
       }
       if(attackChooserNumberThing == 2){
            _stateMachine.ChangeState<B2ScalpelShot>(); 
       }
       if(attackChooserNumberThing == 3){
            _stateMachine.ChangeState<B2Hammer>();
       }
    }
   } 

}
