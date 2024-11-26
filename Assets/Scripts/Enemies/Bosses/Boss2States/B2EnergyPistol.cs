using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2EnergyPistol : State
{
protected B2FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
  protected bool hasWaited;
  protected Animator animator;
    [SerializeField] private GameObject _bulletPrefab;
  [SerializeField] private Transform _BossOffset;

  [SerializeField] private Transform _BulletSpawnOffset;
    // The velocity of which the bullet travels.
  [SerializeField] private float _bulletSpeed;

  [SerializeField] private float _bulletAmount;
   
    // time between each shot.
   [SerializeField] private float _timeBetweenShots;
    private float _lastFireTime;
   IEnumerator wait(){ 
      yield return new WaitForSeconds(1);
      hasWaited = true; 
   }


   private void FireBullet() {
        GameObject bullet = Instantiate(_bulletPrefab, _BulletSpawnOffset.position, transform.rotation);
        _lastFireTime = Time.time;
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * transform.up;
        Destroy(bullet, 1.5f); //destroy bullet after 1 second of shooting them    
    }   

     IEnumerator fireVolley(){
        float timeSinceLastFire = Time.time - _lastFireTime;
        if (timeSinceLastFire >= _timeBetweenShots || _lastFireTime == 0) {
            for(int i = 0; i < _bulletAmount; i++){
            FireBullet();
            yield return new WaitForSeconds(_timeBetweenShots);      
            }
        } 
        yield return new WaitForSeconds(3);
        hasWaited = true;   
    }


    public override void Enter(){
        _stateMachine = GetComponent<B2FSM>(); 
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        StartCoroutine(fireVolley());
        StartCoroutine(wait());

    }

    public override void Exit(){} 
    public override void Tick(){
        if(hasWaited){
             _stateMachine.ChangeState<B2Weakened>();
        }
    }
}
