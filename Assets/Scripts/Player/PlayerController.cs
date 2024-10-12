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

    private float _DashCounter;
    private float _DashCoolDownCounter; //timers for dash cooldown functionality

    private Vector2 _Movement; //raw movement
    private Vector2 _SmoothedMovement; //damped movement
    private Vector2 _SmoothedMovementVelocity; //speed of damping

    private bool _isFrenzied;
    private float _FrenzyMeter;
    [SerializeField] private float _FrenzyMeterMax;
    private bool _isInvincible;

    private Rigidbody2D _Rigidbody;

    private void Awake() {
        _Rigidbody =  GetComponent<Rigidbody2D>();
        _ActiveSpeed = _Speed;
        _FrenzyMeter = 100;
        _power = _standardDamage;
    }

    public void onMovement(InputAction.CallbackContext context){
        _Movement = context.ReadValue<Vector2>(); 
    }

    public void onDash(InputAction.CallbackContext context){
       
        if(_DashCoolDownCounter <= 0 && _DashCounter <= 0){ //if cooldown is 0 and if the counter is 0, Dash!
            _ActiveSpeed = _DashSpeed;
            _DashCounter = _DashLength;
        } 
  
        
    }

    public void onFrenzy(InputAction.CallbackContext context){
        //if(_isFrenzied = true && _FrenzyMeter => 100){} 
    }

    
    private void OnTriggerEnter2D (Collider2D collision) { // if enemy touches you, take damage 
        if (collision.GetComponent<EnemyMovement>() && _isInvincible == false) {
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

            if(_DashCounter <= 0){ 
                _ActiveSpeed = _Speed;
                _DashCoolDownCounter = _DashCoolDown;
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
        _isInvincible = true;
        _FrenzyMeter = 50;
    }

    public void ExitFrenzyMode(){
        // To be called when the player is to exit frenzy mode.
        _isFrenzied = false;
        _isInvincible = false;
    }

    public void ChangeFrenzyGauge(float increase){
        // Increases or decreases the frenzy gauge's value.
        if (_isFrenzied == true) _FrenzyMeter += increase;
    }

    public void ResetPlayerPower() {
        // Resets player's power to default level.
        _power = _standardDamage;
    }
}
