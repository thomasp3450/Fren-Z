using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2SyringeAttack : MonoBehaviour {

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
       
       AudioManager.Instance.PlaySFX("BloodBomb");

        if (collision.GetComponent<PlayerController>()) {
            gameObject.GetComponent<Animator>().Play("Boss2SyringeExplosion");
            _hasDetonated = true;
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0,0);
            gameObject.transform.localScale = new Vector3((float)7.5, (float)7.5,0);
            _collider.size = new Vector2((float)0.5, (float)0.5);
            collision.GetComponent<SpriteFlash>().StartFlash((float)0.24, new Color((float)255,(float)0.0,(float)0.0), 1);
            if (collision.GetComponent<PlayerController>()._isFrenzied) {
                if (!collision.GetComponent<PlayerController>()._gaugeInvincible) {
                    collision.GetComponent<SpriteFlash>().StartFlash((float)0.24, new Color((float)255,(float)0.0,(float)0.0), 1);
                    collision.GetComponent<PlayerController>().ChangeFrenzyGauge(-5);
                }
            } else {
                if (collision.GetComponent<HealthController>()._isInvincible) {
                    collision.GetComponent<SpriteFlash>().StartFlash((float)0.24, new Color((float)255,(float)0.0,(float)0.0), 1);
                    collision.GetComponent<HealthController>().TakeDamage(1);
                    collision.GetComponent<HealthController>().InitIFrames();
                }
            }

            collision.GetComponent<HealthController>().InitIFrames();
        }
        if(collision.gameObject.tag == "Walls"){ //prevent bullet wall passthrough
            gameObject.GetComponent<Animator>().Play("Boss2SyringeExplosion");
            _hasDetonated = true;
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0,0);
            gameObject.transform.localScale = new Vector3((float)7.5, (float)7.5,0);
            _collider.size = new Vector2((float)0.5, (float)0.5);
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
