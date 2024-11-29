using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B3SmallGun : State
{
     // The bullet object to be instantiated.
    [SerializeField]    
    private GameObject _bulletPrefab;
    
    [SerializeField]
    private Transform _EnemyOffset;  

    [SerializeField]
    private Transform _BulletSpawnOffset;
    // The velocity of which the bullet travels.
    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _bulletAmount;
   
    // Frames between each shot.
    [SerializeField]
    private float _timeBetweenShots;
    private float _lastFireTime;

  protected B3FSM _stateMachine; //instantiate the FSM 
  protected HealthController healthController;
  protected PlayerAwarenessController playerAwarenessController;
   protected bool hasWaited;
   protected Animator animator;
   

    private void FireBullet() {
        GameObject bullet = Instantiate(_bulletPrefab, _BulletSpawnOffset.position, transform.rotation);
        _lastFireTime = Time.time;
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * transform.up;
        Destroy(bullet, 1.5f); //destroy bullet after 1 second of shooting them    
    }    


   public override void Enter(){
        _stateMachine = GetComponent<B3FSM>(); 
        healthController = GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        animator = GetComponent<Animator>();
        hasWaited = false;
    }

   public override void Exit(){
   
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

}
