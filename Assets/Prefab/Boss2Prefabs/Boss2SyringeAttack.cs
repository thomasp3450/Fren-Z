using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2SyringeAttack : MonoBehaviour {

    [SerializeField] CapsuleCollider2D collider;
    float _detonationTimer = 360; // Time for detonation to occur after throw
    float _expirationTimer = 10; // Time for detonated bomb's hitbox to be activated
    public bool _hasDetonated = false; // Activates detonation
    [SerializeField] float _sizeDetMultipliedX; // The horizontal size of the hitbox when detonated.
    [SerializeField] float _sizeDetMultipliedY; // The vertical size of the hitbox when detonated.
    Vector2 _explodedSize;
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
        _sizeDetMultipliedX = collider.size.x * 3.5f;
        _sizeDetMultipliedY = collider.size.y * 3.5f;
        _explodedSize = new Vector2(_sizeDetMultipliedX, _sizeDetMultipliedY);
    }

    void Update() {
        if (!_hasDetonated) _detonationTimer--;
        if (_detonationTimer <= 0) {
            gameObject.GetComponent<Animator>().Play("isDetonating");
            _hasDetonated = true;
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0,0);
            collider.size = _explodedSize;
        }
        if (_hasDetonated) _expirationTimer--;
        if (_expirationTimer <= 0) Destroy(gameObject, (float)0.4);
    }

    
    public void Explode(){
        Collider2D playercollision = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
        GameObject collision =  GameObject.FindWithTag("Player");
        gameObject.GetComponent<Animator>().Play("Boss2SyringeExplosion"); //boom
        _hasDetonated = true;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0,0);
        gameObject.transform.localScale = _explodedSize;
        collider.size = _explodedSize;

       if (collider.IsTouching(playercollision)) { //damage
          
            
            if (collision.GetComponent<PlayerController>()._isFrenzied) {
                if (collision.GetComponent<PlayerController>()._gaugeInvincible) {
                    collision.GetComponent<PlayerController>().ChangeFrenzyGauge((float)-1);
                }
            } else {
                if (!collision.GetComponent<HealthController>()._isInvincible) {
                    collision.GetComponent<SpriteFlash>().StartFlash((float)0.12, new Color((float)255,(float)0.0,(float)0.0), 1);
                    collision.GetComponent<HealthController>().TakeDamage(1);
                    collision.GetComponent<HealthController>().InitIFrames();
                }
            }

            collision.GetComponent<HealthController>().InitIFrames();
        }
    }
    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.GetComponent<EnemyMovement>()) {
            // collision.GetComponent<HealthController>().ExitIFrames();
        }
    }

    
}
