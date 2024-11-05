using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1Slam : State
{
    // The bullet object to be instantiated.
    [SerializeField] private GameObject _slamPrefab;

    [SerializeField] private Transform _EnemyOffset;  
    protected Animator animator;
    protected StateMachine _stateMachine; //instantiate the FSM
    protected bool hasWaited; 

  IEnumerator slam(){
    GameObject slam = Instantiate(_slamPrefab, _EnemyOffset.position, transform.rotation); 
    Destroy(slam, 1); 
    yield return new WaitForSeconds(2); 
    hasWaited = true;
  }

  IEnumerator wait(){ 
    yield return new WaitForSeconds(2); 
    hasWaited = true;
  }
 
   public override void Enter(){
    _stateMachine = GetComponent<StateMachine>(); 
    animator = GetComponent<Animator>();
    animator.SetBool("IsSlamming", true);
    StartCoroutine(wait());
   }

   public override void Exit(){
   animator.SetBool("IsWeakened", true);
   animator.SetBool("IsSlamming", false);
   }
   

   public override void Tick(){
    if(hasWaited){
        _stateMachine.ChangeState<B1Weakened>();
    }
   } 
}
