using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Syringe : State
{
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected Animator animator;
  [SerializeField] Transform spawnPoint;
  [SerializeField] Transform Target;

  [SerializeField] GameObject projectilePrefab;
  [SerializeField] AnimationCurve trajectory;

   IEnumerator wait(){ 
      yield return new WaitForSeconds(4);
      hasWaited = true; 
   }

   IEnumerator SyringeShot(){
    yield return new WaitForSeconds(1);
    //SyringeAoE shot = Instantiate()
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
             _stateMachine.ChangeState<B2Weakened>();
        }
    }


}
