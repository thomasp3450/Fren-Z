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
    [SerializeField]
    private float _bulletLifetime; // sets how long a bullet can survive before despawning 
    private float _bulletLifetimeMax;

    // The positional offset of the gun in comparison to the player.
    [SerializeField]
    private Transform _gunOffset;

    // Frames between each shot.
    private float _timeBetweenShots;
    
    // Is the player pressing the shoot button?
    private bool _fireContinuously;
    private float _lastFireTime;
    

    // Start is called before the first frame update
    void Start() {
      
    }

    // Update is called once per frame
    void Update() {
        _bulletLifetime -= Time.deltaTime; //despawn timer
        if (_fireContinuously) {
            float timeSinceLastFire = Time.time - _lastFireTime;
        
            if (timeSinceLastFire >= _timeBetweenShots) {
                _lastFireTime = Time.deltaTime; 
            }
        }
        
    }

    private void FireBullet() {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * transform.up;
        Destroy(bullet, 1); //destroy bullet after 1 second of shooting them 
    
    }



    public void OnFire(InputAction.CallbackContext context) {
        FireBullet();   
    }
}
