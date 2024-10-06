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
        // C
        if (_fireContinuously) {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots) {
                FireBullet();

                _lastFireTime = Time.time;
            }
        }
    }

    private void FireBullet() {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = _bulletSpeed * transform.up;
    }



    private void OnFire(InputValue inputValue) {
        _fireContinuously = inputValue.isPressed;
    }
}
