using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1Volley : State
{
      // The bullet object to be instantiated.
    [SerializeField]    
    private GameObject _bulletPrefab;
  
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
   public override void Enter(){
   _stateMachine = GetComponent<StateMachine>(); 
    healthController = GetComponent<HealthController>();
    playerAwarenessController = GetComponent<PlayerAwarenessController>();
   }

   public override void Exit(){
    
   }

   public override void Tick(){
   
   } 
}
