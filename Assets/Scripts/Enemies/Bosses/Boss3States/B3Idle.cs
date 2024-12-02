using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3Idle : State
{
  protected B3FSM _stateMachine; //instantiate the FSM 
  protected Animator animator;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  int currentPosition;
  public Vector2 distanceToPlayer;
  public float meleeRange;
  public bool inMeleeRange;

  IEnumerator wait(){ 
    yield return new WaitForSeconds(2); 
    hasWaited = true;
  }
  
   public override void Enter(){ 
      hasWaited = false;
      _stateMachine = GetComponent<B3FSM>(); 
      currentPosition = _stateMachine.GetPosition();
      playerAwarenessController = GetComponent<PlayerAwarenessController>();
      StartCoroutine(wait());
      animator = GetComponent<Animator>();
      
   }

   

   public override void Exit(){
   
   }

   public override void Tick(){
    Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    distanceToPlayer = playerPos.position - transform.position;
     
     if(distanceToPlayer.magnitude <= meleeRange){
         inMeleeRange = true;
      }else{
         inMeleeRange = false;
      }
     
     int attackChooserNumberThing = chooseState();

   if(hasWaited && !inMeleeRange){
     
       if(attackChooserNumberThing == 1){
            _stateMachine.ChangeState<B3SmallGun>();
       }
       if(attackChooserNumberThing == 2){
            _stateMachine.ChangeState<B3EnergyGun>(); 
       }
       
    } else if (hasWaited && inMeleeRange){
          if(attackChooserNumberThing == 1){
            _stateMachine.ChangeState<B3Tendril>();
       }
       if(attackChooserNumberThing == 2){
            _stateMachine.ChangeState<B3Spear>(); 
       }
    }
   }

    private int chooseState(){
      // int attackChooserNumberThing = 1; //testing purposes
      // int attackChooserNumberThing = 2; //testing purposes
      int attackChooserNumberThing = Random.Range(1,3);//generates 1, 2 equally since it is (MinInclusive, MaxExclusive)
      return attackChooserNumberThing;
    }



}
