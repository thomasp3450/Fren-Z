using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Syringe : State
{
   protected B2FSM _stateMachine; //instantiate the FSM 
   protected B2Dash b2Dash;
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  
  protected Animator animator;
  [SerializeField] Transform spawnPoint; //boss 
  [SerializeField] Transform Target1; //dashpos1
  [SerializeField] Transform Target2; //dashpos2
  [SerializeField] Transform Target3; //dashpos3

  [SerializeField] GameObject projectilePrefab;
  [SerializeField] private float ProjectileSpeed, projectileHeight;
  [SerializeField] AnimationCurve trajectory;
  [SerializeField] AnimationCurve trajectoryLinearCorrection;
  [SerializeField] AnimationCurve ProjectileSpeedCurve;

 


   IEnumerator wait(){ 
      yield return new WaitForSeconds(4);
      hasWaited = true; 
   }

   


   IEnumerator SyringeShot(Transform t1, Transform t2){
        yield return new WaitForSeconds(1);
        SyringeAoE projectile1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<SyringeAoE>();
        projectile1.InitializeProjectile(t1, ProjectileSpeed, projectileHeight);
        projectile1.InitializeAnimationCurves(trajectory, trajectoryLinearCorrection, ProjectileSpeedCurve);
        // SyringeAoE projectile2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<SyringeAoE>();
        // projectile1.InitializeProjectile(t2, ProjectileSpeed, projectileHeight);
        // projectile2.InitializeAnimationCurves(trajectory, trajectoryLinearCorrection, ProjectileSpeedCurve);
    }
    
    public override void Enter(){
       _stateMachine = GetComponent<B2FSM>(); 
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        b2Dash = gameObject.GetComponent<B2Dash>(); 
        animator = GetComponent<Animator>();
        int position = _stateMachine.GetPosition();
        
        if(position == 1){
            StartCoroutine(SyringeShot(Target3, Target1));
        }
        if(position == 2){
            StartCoroutine(SyringeShot(Target1, Target3));
        }
        if(position == 3){
            StartCoroutine(SyringeShot(Target2, Target1));
        }

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
