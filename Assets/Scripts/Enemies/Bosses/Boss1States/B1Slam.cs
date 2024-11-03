using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1Slam : State
{
      // The bullet object to be instantiated.
    [SerializeField]    
    private GameObject _slamPrefab;

    [SerializeField]
    private Transform _EnemyOffset;  
  protected StateMachine _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;

  IEnumerator slam(){
    GameObject slam = Instantiate(_slamPrefab, _EnemyOffset.position, transform.rotation); 
    Destroy(slam, 1.4f); //slam effect lasts for 1.4 seconds before despawning 
    yield return new WaitForSeconds(2); 
  }
 
   public override void Enter(){
    _stateMachine = GetComponent<StateMachine>(); 
    healthController = GetComponent<HealthController>();
    playerAwarenessController = GetComponent<PlayerAwarenessController>();
    
   }

   public override void Exit(){
   
   }
   

   public override void Tick(){
    StartCoroutine(slam());
    _stateMachine.ChangeState<B1Weakened>();
   } 
}
