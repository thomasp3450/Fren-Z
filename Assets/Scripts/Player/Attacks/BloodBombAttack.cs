using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBombAttack : MonoBehaviour {
    [SerializeField] CapsuleCollider2D _collider;
    float _detonationTimer = 360; // Time for detonation to occur after throw
    float _expirationTimer = 10; // Time for detonated bomb's hitbox to be activated
    public bool _hasDetonated = false; // Activates detonation
    [SerializeField] float _sizeDetMultipliedX; // The horizontal size of the hitbox when detonated.
    [SerializeField] float _sizeDetMultipliedY; // The vertical size of the hitbox when detonated.
    Vector2 _explodedSize;
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
        _sizeDetMultipliedX = _collider.size.x * 3;
        _sizeDetMultipliedY = _collider.size.y * 3;
        _explodedSize = new Vector2(_sizeDetMultipliedX, _sizeDetMultipliedY);
    }

    void Update() {
        if (!_hasDetonated) _detonationTimer--;
        if (_detonationTimer <= 0) {
            gameObject.GetComponent<Animator>().Play("isDetonating");
            _hasDetonated = true;
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0,0);
            gameObject.transform.localScale = new Vector3((float)7.5, (float)7.5,0);
            _collider.size = new Vector2((float)0.5, (float)0.5);
        }
        if (_hasDetonated) _expirationTimer--;
        if (_expirationTimer <= 0) Destroy(gameObject, (float)0.4);
    }

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) {
            gameObject.GetComponent<Animator>().Play("isDetonating");
            _hasDetonated = true;
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0,0);
            gameObject.transform.localScale = new Vector3((float)7.5, (float)7.5,0);
            _collider.size = new Vector2((float)0.5, (float)0.5);
            collision.GetComponent<HealthController>().TakeDamage(10);
            if (collision.GetComponent<B1Slam>()) collision.GetComponent<HealthController>().TakeDamage(30);
            collision.GetComponent<HealthController>().InitIFrames();
        }
        if(collision.gameObject.tag == "Walls"){ //prevent bullet wall passthrough
            Destroy(gameObject, 0.01f);
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
