using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2ScalpelShot : State
{
  protected B2FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited, hasShot;
  protected Animator animator;
  int currentPosition;
  [SerializeField] private Transform _BossOffset; 
  [SerializeField] private GameObject scalpelProjectile;
  
  [SerializeField] private float ProjectileSpeed;
  private GameObject scalpel;
   

   IEnumerator wait(){ 
      yield return new WaitForSeconds(1);
      if(hasShot){
        hasWaited = true; 
      }
      
   }

    IEnumerator ScalpelShot(){
        if(scalpel != null && !hasShot){
            hasShot = true;
            scalpel = Instantiate(scalpelProjectile, _BossOffset.position, transform.rotation); 
            Rigidbody2D s1RigidBody = scalpel.GetComponent<Rigidbody2D>();
            s1RigidBody.velocity = ProjectileSpeed * transform.up;
            hasShot = true;
            yield return new WaitForSeconds(2);
        }
        
    }
            

    public override void Enter(){
       _stateMachine = GetComponent<B2FSM>(); 
        healthController = GetComponent<HealthController>();
        currentPosition = _stateMachine.GetPosition();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        hasShot = false;
        hasWaited = false;
        animator.Play("boss-2-scalpel", 0, 0);
        StartCoroutine(ScalpelShot());
        StartCoroutine(wait());
    }

    public override void Exit(){} 
    public override void Tick(){
        if(hasWaited){
             _stateMachine.ChangeState<B2Weakened>();
        }
    }
}
