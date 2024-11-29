using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShoot : MonoBehaviour
{
    // The bullet object to be instantiated.
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _meleePrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _BulletOffset; 
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] public float meleeRange;
    Animator animator;
    private float _lastFireTime;
    private PlayerAwarenessController _awareOfPlayerController;
    public bool inMeleeRange;
    public float playerDistance;

     void Start() {
        animator = GetComponent<Animator>();
        _awareOfPlayerController = GetComponent<PlayerAwarenessController>();
    }  

    void Update() {
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        var distanceToPlayer = playerPos.position - transform.position;
        playerDistance = distanceToPlayer.magnitude;
        if(playerDistance <= meleeRange){
            inMeleeRange = true;
        }else{
            inMeleeRange = false;
        }
        float timeSinceLastFire = Time.time - _lastFireTime;
        if (_awareOfPlayerController.AwareOfPlayer && !inMeleeRange) {
            if (timeSinceLastFire >= _timeBetweenShots || _lastFireTime == 0) {
                FireBullet();  
            }
        } else if (_awareOfPlayerController.AwareOfPlayer && inMeleeRange){
            
            if (timeSinceLastFire >= _timeBetweenShots || _lastFireTime == 0) {
                MeleeAttack();
            }
           
        }
    }

    private void FireBullet() {
       
        GameObject bullet = Instantiate(_bulletPrefab, _BulletOffset.position, transform.rotation);
        AudioManager.Instance.PlaySFX("EnergyShot");
        animator.Play("Fire");
        _lastFireTime = Time.time;
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * transform.up;
        Destroy(bullet, 2); //destroy bullet after 2 seconds of shooting them 
        
    }

    private void MeleeAttack(){
        GameObject shove = Instantiate(_meleePrefab, transform.position, transform.rotation);
        AudioManager.Instance.PlaySFX("LightAttack");
        animator.Play("Shove");
        Destroy(shove, 0.4f); 
    }

}
