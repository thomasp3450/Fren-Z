using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Syringe : State
{
   protected B2FSM _stateMachine; //instantiate the FSM 
   protected B2Dash b2Dash;
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited, hasShot, hasExploded;
  
  protected Animator animator;
  [SerializeField] Transform spawnPoint; //boss 
  [SerializeField] Transform Target1; //dashpos1
  [SerializeField] Transform Target2; //dashpos2
  [SerializeField] Transform Target3; //dashpos3

  [SerializeField] GameObject projectilePrefab;
  [SerializeField] private float ProjectileSpeed;

  GameObject syringe1, syringe2;
  int position;


   IEnumerator wait(){ 
      yield return new WaitForSeconds(4);
      hasWaited = true; 
   }

   


   IEnumerator SyringeShot(Transform t1, Transform t2){
        if(syringe1 != null && syringe2 != null && !hasExploded){
            hasShot = true;
            yield return new WaitForSeconds(2);
            float step = ProjectileSpeed * Time.deltaTime;

            syringe1.transform.position = Vector2.MoveTowards(syringe1.transform.position, t1.position, step);
            syringe2.transform.position = Vector2.MoveTowards(syringe2.transform.position, t2.position, step);

            if(syringe1.transform.position == t1.position){
                syringe1.GetComponent<Boss2SyringeAttack>().Explode();
                hasExploded = true;
            }

            if(syringe2.transform.position == t2.position){
                syringe2.GetComponent<Boss2SyringeAttack>().Explode();
                hasExploded = true;
            }
        }
        
    }
        
    
    public override void Enter(){
        _stateMachine = GetComponent<B2FSM>(); 
        
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        position = _stateMachine.GetPosition();
        hasWaited = false;
        hasShot = false;
        hasExploded = false;
        syringe1 = Instantiate(projectilePrefab, transform.position, transform.rotation);
        syringe2 = Instantiate(projectilePrefab, transform.position, transform.rotation);
    }

    public override void Exit(){

    } 
    public override void Tick(){

        if(syringe1 != null && syringe2 != null){
            if(position == 1){
                StartCoroutine(SyringeShot(Target3, Target2));
            }
            if(position == 2){
                StartCoroutine(SyringeShot(Target1, Target3));
            }
            if(position == 3){
                StartCoroutine(SyringeShot(Target2, Target1));
            }
        }
        
        if(hasWaited){
             _stateMachine.ChangeState<B2Weakened>();
        }
    }


}
