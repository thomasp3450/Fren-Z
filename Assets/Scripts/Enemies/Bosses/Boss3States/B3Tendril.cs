using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3Tendril : State
{
    protected B3FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected Animator animator;
  int currentPosition;
  [SerializeField] private Transform _BossOffset;
  [SerializeField] public GameObject TendrilPrefab;

   IEnumerator Tendril(){
        GameObject tendril = Instantiate(TendrilPrefab, _BossOffset.position, transform.rotation); 
        Destroy(tendril, 1); 
        yield return new WaitForSeconds(2); 
        hasWaited = true;
   }

    public override void Enter(){
        _stateMachine = GetComponent<B3FSM>(); 
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        animator.Play("Tendril");
        AudioManager.Instance.PlaySFX("Boss1Slam");
    }

    public override void Exit(){} 
    public override void Tick(){
        if(hasWaited){
             _stateMachine.ChangeState<B3Weakened>();
        }
    }
}
