using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Dash : State
{
      protected StateMachine _stateMachine; //instantiate the FSM 
  protected Animator animator;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected int currentPosition;
  public float bossSpeed;
    [SerializeField] private Transform _BossOffset; 
  
    [SerializeField] public Transform pos1, pos2, pos3;
  
 
   public override void Enter(){ 
      currentPosition = 1;
      hasWaited = false;
      _stateMachine = GetComponent<StateMachine>(); 
      playerAwarenessController = GetComponent<PlayerAwarenessController>();
      animator = GetComponent<Animator>();
      pos1 = this.gameObject.transform.GetChild(0);
      pos2 = this.gameObject.transform.GetChild(1);
      pos3 = this.gameObject.transform.GetChild(2);
      //pick destination
        if(currentPosition == 1){
            Dashdecision(1);   
        }
        
        else if(currentPosition == 2){
            Dashdecision(2);
        }
        
        else if(currentPosition == 3){
            Dashdecision(3);
        }
      //dash to destination
      StartCoroutine(Dash());
      //decide distance in Tick and transition to attack
   }

   

   public override void Exit(){
    
   }

   public override void Tick(){
    if(hasWaited){
 
    //    Vector2 PlayerDistance = playerAwarenessController.getPlayerDistance();
    //         if(PlayerDistance.magnitude > 5){
    //             _stateMachine.ChangeState<B2EnergyPistol>();
    //         } else{
    //             _stateMachine.ChangeState<B2Spin>();
    //         }
    } 
   }

    IEnumerator wait(){ //wait for a 3 seconds in idle before transitioning to the attacks
        yield return new WaitForSeconds(3); 
        hasWaited = true;
    }

    IEnumerator Dash(){
        float step = bossSpeed * Time.deltaTime;
        if(currentPosition == 1){
            yield return new WaitForSeconds(1);
            transform.position = Vector2.MoveTowards(transform.position, pos1.transform.position, step);
        }
        
        else if(currentPosition == 2){
            yield return new WaitForSeconds(1);
            transform.position = Vector2.MoveTowards(transform.position, pos2.transform.position, step);
        }
        
        else if(currentPosition == 3){
            yield return new WaitForSeconds(1);
            transform.position = Vector2.MoveTowards(transform.position, pos3.transform.position, step);
        }


    }


  private int Dashdecision(int currentPos){
    int dashChooserNumberThing = Random.Range(1,3);
    
    if(currentPos == 1){
        if(dashChooserNumberThing == 1){
            currentPosition = 2;
        }
        if(dashChooserNumberThing == 2){
            currentPosition = 3;
        }
    }
    
    else if(currentPos == 2){
        if(dashChooserNumberThing == 1){
            currentPosition = 1;
        }
        if(dashChooserNumberThing == 2){
            currentPosition = 3;
        }
    }
    
    else if(currentPos == 3){
        if(dashChooserNumberThing == 1){
            currentPosition = 1;
        }
        if(dashChooserNumberThing == 2){
            currentPosition = 2;
        }
    }
    return currentPosition;   
  }


  

}
