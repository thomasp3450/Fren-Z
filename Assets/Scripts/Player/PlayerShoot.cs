using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour {
    // The bullet object to be instantiated.
    [SerializeField]
    private GameObject _bulletPrefab;

    // The velocity of which the bullet travels.
    [SerializeField]
    private float _bulletSpeed;

    private float _bulletLifetimeMax;

   //where the player is
    [SerializeField]
    private Transform _gunOffset;


    // Frames between each shot.
    [SerializeField]
    private float _timeBetweenShots;
    
    private float _lastFireTime;
    
    Animator animator;

   


    // Start is called before the first frame update
    void Start() {
      animator = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update() {

    }

    private void FireBullet() {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        _lastFireTime = Time.time;
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * transform.up;
        Destroy(bullet, 1); //destroy bullet after 1 second of shooting them 
        
    }



    public void OnFire(InputAction.CallbackContext context) {
        // Debug.Log(gameObject.GetComponent<PlayerController>()._isFrenzied);
        if (!gameObject.GetComponent<PlayerPause>()._pauseMenu.activeSelf && 
        !gameObject.GetComponent<PlayerGameOver>()._gameoverMenu.activeSelf && !gameObject.GetComponent<PlayerController>()._isFrenzied) {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire >= _timeBetweenShots || _lastFireTime == 0) {
                FireBullet();  
                animator.SetBool("Shooting", true);
                
            }else{
                animator.SetBool("Shooting",false);
            }
        }
    }
}
