using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBombAttack : MonoBehaviour {
    [SerializeField] CapsuleCollider2D _collider;
    float _detonationTimer = 10; // Time for detonation to occur after throw
    float _expirationTimer = 10; // Time for detonated bomb's hitbox to be activated
    public bool _hasDetonated = false; // Activates detonation
    [SerializeField] float _sizeDetMultipliedX; // The horizontal size of the hitbox when detonated.
    [SerializeField] float _sizeDetMultipliedY; // The vertical size of the hitbox when detonated.
    Vector2 _explodedSize;

    void Awake() {
        _sizeDetMultipliedX = _collider.size.x * 2;
        _sizeDetMultipliedY = _collider.size.y * 2;
        _explodedSize = new Vector2(_sizeDetMultipliedX, _sizeDetMultipliedY);
    }

    void Update() {
        if (_hasDetonated) _expirationTimer--;
        if (_expirationTimer <= 0) Destroy(gameObject, 0.01f);
    }

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            _hasDetonated = true;
            _collider.size = new Vector2(_explodedSize.x, _explodedSize.y);
            collision.GetComponent<HealthController>().TakeDamage(10);
            collision.GetComponent<HealthController>().InitIFrames();
        }

    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<EnemyMovement>()) {
            // collision.GetComponent<HealthController>().ExitIFrames();
        }
    }

    void OnDestroy() {
    }
}
