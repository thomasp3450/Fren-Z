using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShoot : MonoBehaviour
{
    // The bullet object to be instantiated.
    [SerializeField]    
    private GameObject _bulletPrefab;


    // The velocity of which the bullet travels.
    [SerializeField]
    private float _bulletSpeed;

    // The positional offset of the enemy
    [SerializeField]
    private Transform _EnemyOffset;

    
    // The positional offset of the Barrel
    [SerializeField]
    private Transform _BulletOffset;

    
   
    // Frames between each shot.
    [SerializeField]
    private float _timeBetweenShots;

    Animator animator;

    private float _lastFireTime;

    private PlayerAwarenessController _awareOfPlayerController;

     void Start() {
        animator = GetComponent<Animator>();
        _awareOfPlayerController = GetComponent<PlayerAwarenessController>();
        
    }  

    void Update() {
        if (_awareOfPlayerController.AwareOfPlayer) {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire >= _timeBetweenShots || _lastFireTime == 0) {
                FireBullet();  
                animator.SetBool("isShooting", true);
            }else{
                animator.SetBool("isShooting",false);
            }
        }
    }

    private void FireBullet() {
       
        GameObject bullet = Instantiate(_bulletPrefab, _BulletOffset.position, transform.rotation);
        AudioManager.Instance.PlaySFX("Gunshot");
        _lastFireTime = Time.time;
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * transform.up;
        Destroy(bullet, 1); //destroy bullet after 1 second of shooting them 
        
    }
 
}
