using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{
    [SerializeField] float _Speed; //how fast you go normally
    private float _ActiveSpeed; //how fast you are currently going
    [SerializeField] float _DashSpeed; //how fast you dash
    [SerializeField] float _DashLength; // how long to dash for
    [SerializeField] float _DashCoolDown; // how long before you can dash again
    [SerializeField] float _RotationSpeed; //how quickly your character rotates to face your input direction
    [SerializeField] float _standardDamage;
    private float _power;
    private float _DashCounter; // for how long to dash
    private float _DashCoolDownCounter; // how long until you can dash again
    private Vector2 _Movement; //raw movement
    private Vector2 _SmoothedMovement; //damped movement
    private Vector2 _SmoothedMovementVelocity; //speed of damping

    public bool _isFrenzied;
    private float _FrenzyMeter;
    [SerializeField] private float _FrenzyMeterMax;
    private bool _isAttacking = false;

    private Rigidbody2D _Rigidbody;
    [SerializeField] public GameObject _frenzyBar;
    [SerializeField] public GameObject _frenzyBarBackground;

    Animator animator;

    // The bullet object to be instantiated.
    [SerializeField]
    private GameObject _lightAttack;
    private float _lastLightAttackTime;
    [SerializeField]
    private GameObject _heavyAttack;

    private void Awake() {
        _Rigidbody =  GetComponent<Rigidbody2D>();
        _ActiveSpeed = _Speed;
        _FrenzyMeter = 50;
        _power = _standardDamage;
        animator = GetComponent<Animator>();
    }

    public void onMovement(InputAction.CallbackContext context){
        _Movement = context.ReadValue<Vector2>(); 
    }

    public void onDash(InputAction.CallbackContext context){
       
        if(_DashCoolDownCounter <= 0 && _DashCounter <= 0){ //if cooldown is 0 and if the counter is 0, Dash!
            _ActiveSpeed = _DashSpeed;
            _DashCounter = _DashLength;
            if (_isFrenzied) GetComponent<HealthController>().InitIFrames();
        } else if (GetComponent<HealthController>()._isInvincible) {
            GetComponent<HealthController>().ExitIFrames();
        }
  
        
    }

    public void onFrenzy(InputAction.CallbackContext context) {
        Debug.Log("Frenzy value: " + _FrenzyMeter);
        if (_isFrenzied == true && _FrenzyMeter >= 100 ) {
            gameObject.GetComponent<HealthController>().AddHealth(1);
            ExitFrenzyMode();
        }
        else{
            EnterFrenzyMode();
        }
    }

    
    private void OnTriggerEnter2D (Collider2D collision) { 
        if (!_isFrenzied) {
            // if enemy touches you, take damage 
            if (collision.GetComponent<EnemyMovement>() && GetComponent<HealthController>()._isInvincible == false) {
                Debug.Log("Player's HP was reduced.");
                gameObject.GetComponent<HealthController>().TakeDamage(1);
            }
        } else if (collision.GetComponent<EnemyMovement>()) {
            // Takes a small fraction of the player's frenzy gauge
            _FrenzyMeter -= 1;
            Debug.Log("Player's frenzy gauge was reduced.");
        }
        if (collision.GetComponent<EnemyMovement>()) {
            if (_DashCounter > 0 && _isFrenzied) {
                // The dash attack is activated when the player is in Frenzy state and dashing.
                collision.GetComponent<HealthController>().TakeDamage((float)0.5);
            }
        }
    }


    private void FixedUpdate() { //move and rotate with input
        SetPlayerVelocity();
        if (!_isAttacking) RotateWithDirection();
        // Debug.Log("Active speed: " + _ActiveSpeed);
        
        if(_isFrenzied){
            _FrenzyMeter -= Time.deltaTime/2;
        }
        if(_DashCounter > 0){ // dash cooldown counter calculation
            _DashCounter -= Time.deltaTime;
            animator.SetBool("Dashing", true);
            if(_DashCounter <= 0){ 
                _ActiveSpeed = _Speed;
                _DashCoolDownCounter = _DashCoolDown;
                animator.SetBool("Dashing", false);
            }

        }

        if(_DashCoolDownCounter > 0){
             _DashCoolDownCounter -= Time.deltaTime;
        }

        // If player fills the Frenzy gauge.
        if (_isFrenzied && _FrenzyMeter >= _FrenzyMeterMax) { 
            gameObject.GetComponent<HealthController>().AddHealth(1);
            ExitFrenzyMode();
        }
        // Player depletes the Frenzy gauge and loses.
        if (_isFrenzied && _FrenzyMeter <= 0) {
            _FrenzyMeter = 0;
            gameObject.SetActive(false);
        }

        _frenzyBar.GetComponent<Image>().fillAmount = _FrenzyMeter / _FrenzyMeterMax;
    }

    private void SetPlayerVelocity(){ // creates smoother movement by transitioning vectors over 0.05 seconds
        _SmoothedMovement = Vector2.SmoothDamp(
        _SmoothedMovement, 
        _Movement, 
        ref _SmoothedMovementVelocity, 
        0.05f); 
       
        _Rigidbody.velocity = _SmoothedMovement * _ActiveSpeed;
        animator.SetFloat("Velocity", Mathf.Abs(_SmoothedMovement.x + _SmoothedMovement.y)); 
    }

    private void RotateWithDirection(){ //rotates player sprite to look towards where moving
        if(_Movement != Vector2.zero){
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _SmoothedMovement);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime); 

            _Rigidbody.MoveRotation(rotation);

        }
    }

    public void SetPlayerDamageMultiplier(float damageMultiplier){
        // Multiplies the player's damage by a float.
        _power *= damageMultiplier;
    }

    public void EnterFrenzyMode(){
        // To be called when the player is to enter frenzy mode.
        Debug.Log("Frenzy mode entered.");
        _isFrenzied = true;
        GetComponent<HealthController>()._isInvincible = true;
        _FrenzyMeter = _FrenzyMeterMax/2;
        // Debug.Log(_FrenzyMeter);
        _frenzyBar.SetActive(true);
        _frenzyBarBackground.SetActive(true);
    }

    public void ExitFrenzyMode(){
        // To be called when the player is to exit frenzy mode.
        _isFrenzied = false;
        GetComponent<HealthController>()._isInvincible = false;
        _frenzyBar.SetActive(false);
        _frenzyBarBackground.SetActive(false);
    }

    public void ChangeFrenzyGauge(float increase){
        // Increases or decreases the frenzy gauge's value.
        Debug.Log("Change in player's frenzy gauge. " + _FrenzyMeter + "/" + _FrenzyMeterMax);
        if (_isFrenzied == true) _FrenzyMeter += increase;
    }

    public void ResetPlayerPower() {
        // Resets player's power to default level.
        _power = _standardDamage;
    }

    public IEnumerator ComboAttack() {
        // Coroutine for the multi-hitting combo attack.
        // Stops player
        _ActiveSpeed = 0;
        // The three hits
        _lightAttack.SetActive(true);
        yield return new WaitForSeconds(.01f);
        _lightAttack.SetActive(false);
        yield return new WaitForSeconds(.01f);
        _lightAttack.SetActive(true);
        yield return new WaitForSeconds(.01f);
        _lightAttack.SetActive(false);
        yield return new WaitForSeconds(.01f);
        _lightAttack.SetActive(true);
        yield return new WaitForSeconds(.01f);
        _lightAttack.SetActive(false);
        // endlag
        yield return new WaitForSeconds(1);
        // Returns player to speed
        _ActiveSpeed = _Speed;
        _isAttacking = false;
    }
    
    public void OnLightAttack() {
        // Initiates the combo attack.
        if (gameObject.GetComponent<PlayerController>()._isFrenzied && !_isAttacking) {
            // Prevents concurrent attack
            _isAttacking = true;
            // A minor cost
            if(_FrenzyMeter > 3) _FrenzyMeter -= 3;
            // Instantiates hitbox prefab
            GameObject lightAttack = Instantiate(_lightAttack, gameObject.transform.position, transform.rotation);
            lightAttack.SetActive(true);
            _lastLightAttackTime = Time.time;
            Rigidbody2D rigidbody = lightAttack.GetComponent<Rigidbody2D>();
            // Starts the attack coroutine to carry out the attack's duration
            StartCoroutine(ComboAttack());
            // Destroys the instance.
            Destroy(lightAttack, 1);
        }
    }

    public IEnumerator HeavyAttack() {
        // Coroutine for the heavy attack.
        if (!_isAttacking) {
            // prevents player from attacking repeatedly.
            _isAttacking = true;
            // Activates the heavy attack's hitbox and keeps it up for 2 seconds, feel free to change
            _heavyAttack.SetActive(true);
            yield return new WaitForSeconds(2);
            _heavyAttack.SetActive(false);
            _ActiveSpeed = _Speed;
            _isAttacking = false;
        }
    }

    public void OnHeavyAttack() {
        // Initiates the heavy attack.
        if (gameObject.GetComponent<PlayerController>()._isFrenzied && !_isAttacking) {
            // A minor cost
            if(_FrenzyMeter > 3) _FrenzyMeter -= 3;
            // Instantiates hitbox prefab
            GameObject heavyAttack = Instantiate(_heavyAttack, gameObject.transform.position, transform.rotation);
            heavyAttack.SetActive(true);
            _lastLightAttackTime = Time.time;
            Rigidbody2D rigidbody = heavyAttack.GetComponent<Rigidbody2D>();
            // Starts the attack coroutine to carry out the attack's duration
            StartCoroutine(HeavyAttack());
            // Destroys the instance.
            Destroy(heavyAttack, 1);
        }
    }
}
