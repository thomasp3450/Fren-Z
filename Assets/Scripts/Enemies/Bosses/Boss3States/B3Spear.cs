using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3Spear : State
{
    protected B3FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected Animator animator;
  int currentPosition;
  [SerializeField] private Transform _BossOffset;
  [SerializeField] public GameObject SlamPrefab;

   IEnumerator SpearThrust(){
        GameObject slam = Instantiate(SlamPrefab, _BossOffset.position, transform.rotation); 
        Destroy(slam, 1); 
        yield return new WaitForSeconds(2); 
        hasWaited = true;
   }

    public override void Enter(){
        _stateMachine = GetComponent<B3FSM>(); 
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        animator.Play("Spear");
        AudioManager.Instance.PlaySFX("LightAttack");
    }

    public override void Exit(){} 
    public override void Tick(){
        if(hasWaited){
            _stateMachine.ChangeState<B3Weakened>();
        }
    }

}
