using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BloodBombAttack : MonoBehaviour {
    [SerializeField] CapsuleCollider2D _collider;
    float _detonationTimer = 360; // Time for detonation to occur after throw
    float _expirationTimer = 10; // Time for detonated bomb's hitbox to be activated
    public bool _hasDetonated = false; // Activates detonation
    [SerializeField] float _sizeDetMultipliedX; // The horizontal size of the hitbox when detonated.
    [SerializeField] float _sizeDetMultipliedY; // The vertical size of the hitbox when detonated.
    Vector2 _explodedSize;
    Animator animator;
    private CinemachineImpulseSource impulseSource;

    void Awake() {
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        _sizeDetMultipliedX = _collider.size.x * 3;
        _sizeDetMultipliedY = _collider.size.y * 3;
        _explodedSize = new Vector2(_sizeDetMultipliedX, _sizeDetMultipliedY);
    }

    void Update() {
        
        if (!_hasDetonated) _detonationTimer--; // Airborne timer for automatic explosion

        if (_detonationTimer <= 0) { // Condition where the bomb flies enough without colliding and therefore explodes

            ScreenShake.Instance.ShakeCamera(impulseSource, .3f);
            AudioManager.Instance.PlaySFX("BloodBomb");

            // Animation plays for the explosion
            gameObject.GetComponent<Animator>().Play("isDetonating");

            // Explosion has now started so the explosion linger timer can start and eventually get rid of the bomb.
            _hasDetonated = true;

            // The rigidbody
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0,0);

            // Hitbox of bomb increases when exploding
            gameObject.transform.localScale = new Vector3((float)7.5, (float)7.5,0);
            _collider.size = new Vector2((float)0.5, (float)0.5);

        }

        // Duration of the explosion to linger.
        if (_hasDetonated) _expirationTimer--;
        if (_expirationTimer <= 0) Destroy(gameObject, (float)0.4);
    }

    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.GetComponent<EnemyMovement>()) { // Condition where the bomb collides with an enemy and will detonate

            ScreenShake.Instance.ShakeCamera(impulseSource, .3f);
            AudioManager.Instance.PlaySFX("BloodBomb");

            // Plays the detonating bomb animation
            gameObject.GetComponent<Animator>().Play("isDetonating");

            // Explosion has now started so the explosion linger timer can start and eventually get rid of the bomb.
            _hasDetonated = true;

            // The rigidbody
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0,0);

            // Increases size of hitbox for the explosion
            gameObject.transform.localScale = new Vector3((float)7.5, (float)7.5,0);
            _collider.size = new Vector2((float)0.5, (float)0.5);

            // The damage itself
            collision.GetComponent<HealthController>().TakeDamage(40);

            /* // Damage multiplies against boss 1
            if (collision.GetComponent<B1Slam>()
                || collision.GetComponent<B2Idle>() || collision.GetComponent<B3Idle>()
            ) collision.GetComponent<HealthController>().TakeDamage(50); */

            // Prevents hitting again
            // collision.GetComponent<HealthController>().InitIFrames();
        }
        if(collision.gameObject.tag == "Walls"){ //prevent bullet wall passthrough

            ScreenShake.Instance.ShakeCamera(impulseSource, .3f);
            AudioManager.Instance.PlaySFX("BloodBomb");

            // Bomb explodes when colliding with wall. Same stuff
            gameObject.GetComponent<Animator>().Play("isDetonating");
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
