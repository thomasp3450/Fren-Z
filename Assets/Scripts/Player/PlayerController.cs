using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private bool _isFrenzied;
    private float _FrenzyMeter;
    [SerializeField] private float _FrenzyMeterMax;

    private Rigidbody2D _Rigidbody;

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
        _FrenzyMeter = 100;
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
        if (_isFrenzied == true && _FrenzyMeter >= 100 ) {
            ExitFrenzyMode();
        }
        else{
            EnterFrenzyMode();
        }
    }

    
    private void OnTriggerEnter2D (Collider2D collision) { // if enemy touches you, take damage 
        if (collision.GetComponent<EnemyMovement>() && GetComponent<HealthController>()._isInvincible == false) {
            gameObject.GetComponent<HealthController>().TakeDamage(1);
        }
    }


    private void FixedUpdate() { //move and rotate with input
        SetPlayerVelocity();
        RotateWithDirection();
        
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
            ExitFrenzyMode();
        }
        // Player depletes the Frenzy gauge and loses.
        if (_isFrenzied && _FrenzyMeter <= 0) {
            _FrenzyMeter = 0;
            gameObject.SetActive(false);
        }
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
        PlayerInput pi = GetComponent<PlayerInput>();
        pi.actions.FindAction("Fire").Disable();
        pi.actions.FindAction("AttackLight").Enable();
        pi.actions.FindAction("AttackHeavy").Enable();
        Debug.Log("Frenzy mode entered.");
        _isFrenzied = true;
        GetComponent<HealthController>()._isInvincible = true;
        _FrenzyMeter = 50;
    }

    public void ExitFrenzyMode(){
        // To be called when the player is to exit frenzy mode.
        _isFrenzied = false;
        GetComponent<HealthController>()._isInvincible = false;
        PlayerInput pi = GetComponent<PlayerInput>();
        pi.actions.FindAction("AttackLight").Disable();
        pi.actions.FindAction("AttackHeavy").Disable();
        pi.actions.FindAction("Fire").Enable();
    }

    public void ChangeFrenzyGauge(float increase){
        // Increases or decreases the frenzy gauge's value.
        Debug.Log("Increase in player's frenzy gauge.");
        if (_isFrenzied == true) _FrenzyMeter += increase;
    }

    public void ResetPlayerPower() {
        // Resets player's power to default level.
        _power = _standardDamage;
    }

    public IEnumerator ComboAttack() {
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
    }
    
    public void ExecuteLightAttack() {
        GameObject lightAttack = Instantiate(_lightAttack, gameObject.transform.position, transform.rotation);
        _lastLightAttackTime = Time.time;
        Rigidbody2D rigidbody = lightAttack.GetComponent<Rigidbody2D>();
        StartCoroutine(ComboAttack());
        Destroy(lightAttack, 1);
    }

    public void ExecuteLightAttack1() {
        // Claw attack. Combo link 1
        _lightAttack.SetActive(true);
    }

    public void ExecuteLightAttack2() {
        // Blade attack. Combo link 2
        _lightAttack.SetActive(true);
    }

    public void ExecuteLightAttack3() {
        // Shield attack. Combo link 3
        _lightAttack.SetActive(true);
    }

    public void ExecuteHeavyAttack() {
        // Narrow, spike attack
        _heavyAttack.SetActive(true);
    }
}
