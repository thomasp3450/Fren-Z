using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Spin : State
{
   protected B2FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  public Vector2 distanceToPlayer;
  protected Animator animator;
  int currentPosition;
  public float meleeRange;
  public bool inMeleeRange;
   IEnumerator wait(){ 
      yield return new WaitForSeconds(1);
      hasWaited = true; 
   }

    public override void Enter(){
        _stateMachine = GetComponent<B2FSM>(); 
        currentPosition = _stateMachine.GetPosition();
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        
        StartCoroutine(wait());
        
    }

   public override void Exit(){
  
   }

   public override void Tick(){
      Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
      var distanceToPlayer = playerPos.position - transform.position;
      
      if(distanceToPlayer.magnitude <= meleeRange){
         inMeleeRange = true;
      }else{
         inMeleeRange = false;
      }

    if(hasWaited && !inMeleeRange){
      int attackChooserNumberThing = chooseState();
       if(attackChooserNumberThing == 1){
            _stateMachine.ChangeState<B2Syringe>();
       }
       if(attackChooserNumberThing == 2){
            _stateMachine.ChangeState<B2ScalpelShot>(); 
       }
       if(attackChooserNumberThing == 3){
            _stateMachine.ChangeState<B2EnergyPistol>();
       }
    } else if (hasWaited && inMeleeRange){
            _stateMachine.ChangeState<B2Hammer>();
    }
   } 

   private int chooseState(){
      // int attackChooserNumberThing = 1; //testing purposes
      // int attackChooserNumberThing = 2; //testing purposes
      // int attackChooserNumberThing = 3; //testing purposes
      int attackChooserNumberThing = Random.Range(1,4);//generates 1, 2, or 3 equally since it is (MinInclusive, MaxExclusive)
      animator.Play("boss2-spin", 0, 0);
      AudioManager.Instance.PlaySFX("Boss2Spin");
      return attackChooserNumberThing;
      }

}
