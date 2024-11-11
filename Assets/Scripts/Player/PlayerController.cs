using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{
    [SerializeField] public float _Speed; //how fast you go normally
    public float _ActiveSpeed; //how fast you are currently going
    [SerializeField] float _DashSpeed; //how fast you dash
    [SerializeField] float _DashLength; // how long to dash for
    [SerializeField] float _DashCoolDown; // how long before you can dash again
    [SerializeField] float _RotationSpeed; //how quickly your character rotates to face your input direction
    [SerializeField] float _standardDamage;
    [SerializeField] public int _amountOfBloodBombs = 0;
    [SerializeField] public int _amountOfSyringes = 0;
    private float _power;
    private float _DashCounter; // for how long to dash
    private float _DashCoolDownCounter; // how long until you can dash again
    private Vector2 _Movement; //raw movement
    private Vector2 _SmoothedMovement; //damped movement
    private Vector2 _SmoothedMovementVelocity; //speed of damping

    public bool _isFrenzied;
    public bool _gaugeInvincible = false;
    private float _FrenzyMeter;
    [SerializeField] private float _FrenzyMeterMax;
    public bool _isAttacking = false; // Will create a public function to check _isAttacking later and make _isattacking private again

    private Rigidbody2D _Rigidbody;
    [SerializeField] public GameObject _frenzyBar;
    [SerializeField] public GameObject _frenzyBarBackground;
    [SerializeField] public GameObject _UIAmountOfBloodBombs;
    [SerializeField] public GameObject _UIAmountOfStimPacks;

    Animator animator;

    [SerializeField] private GameObject _lightAttack;
    private float _lastLightAttackTime;
    public int _comboLink = 0;

    [SerializeField]
    private GameObject _heavyAttack;

    private void Awake() {
        _Rigidbody =  GetComponent<Rigidbody2D>();
        _ActiveSpeed = _Speed;
        _FrenzyMeter = 50;
        _power = _standardDamage;
        animator = GetComponent<Animator>();
        animator.SetBool("Frenzied", false);
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
        // Debug.Log("Frenzy value: " + _FrenzyMeter);
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
                // Debug.Log("Player's HP was reduced.");
                gameObject.GetComponent<HealthController>().TakeDamage(1);
            }
        } else if (collision.GetComponent<EnemyMovement>()) {
            // Takes a small fraction of the player's frenzy gauge
            if (!_gaugeInvincible) _FrenzyMeter -= 2;
            // Debug.Log("Player's frenzy gauge was reduced.");
        }
        if (collision.GetComponent<EnemyMovement>()) {
            if (_DashCounter > 0 && _isFrenzied) {
                // The dash attack is activated when the player is in Frenzy state and dashing.
                collision.GetComponent<HealthController>().TakeDamage((float)3);
            }
        }
    }


    private void FixedUpdate() { //move and rotate with input
        SetPlayerVelocity();
        _UIAmountOfBloodBombs.GetComponent<TMPro.TextMeshProUGUI>().text = "" + _amountOfBloodBombs + "";
        _UIAmountOfStimPacks.GetComponent<TMPro.TextMeshProUGUI>().text = "" + _amountOfSyringes + "";
        RotateWithDirection();
        // Debug.Log("Active speed: " + _ActiveSpeed);
        
        if(_isFrenzied){
            _FrenzyMeter -= Time.deltaTime;
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

        // Resets combo if time has passed since last hit
        if ((Time.time - _lastLightAttackTime) > 2) {
            _comboLink = 0;
        }

        if(_DashCoolDownCounter > 0){
             _DashCoolDownCounter -= Time.deltaTime;
        }

        // Changes player out of frenzy mode after gauge is filled.
        if (_isFrenzied && GetFrenzyMeter() >= GetFrenzyMeterMax()) { 
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

    public float GetFrenzyMeter(){
        return _FrenzyMeter;
    }

    public float GetFrenzyMeterMax(){
        return _FrenzyMeterMax;
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
        if (_isFrenzied == false) _FrenzyMeter = _FrenzyMeterMax/2;
        _isFrenzied = true;
        animator.SetBool("Frenzied", true);
        GetComponent<HealthController>()._isInvincible = true;
        // Debug.Log(_FrenzyMeter);
        _frenzyBar.SetActive(true);
        _frenzyBarBackground.SetActive(true);
    }

    public void ExitFrenzyMode(){
        // To be called when the player is to exit frenzy mode.
        _isFrenzied = false;
        animator.SetBool("Frenzied", false);
        GetComponent<HealthController>()._isInvincible = false;
        _frenzyBar.SetActive(false);
        _frenzyBarBackground.SetActive(false);
    }

    public void ChangeFrenzyGauge(float increase){
        // Increases or decreases the frenzy gauge's value.
        // Debug.Log("Change in player's frenzy gauge. " + _FrenzyMeter + "/" + _FrenzyMeterMax);
        if (_isFrenzied == true) _FrenzyMeter += increase;
    }

    public void ResetPlayerPower() {
        // Resets player's power to default level.
        _power = _standardDamage;
    }

    /* public IEnumerator ComboAttack() {
        // Coroutine for the multi-hitting combo attack.
        // Slows player
        _ActiveSpeed = 5;
        // The three hits
        _lightAttack.SetActive(true);
        animator.SetInteger("ComboInt", 1);
        animator.SetBool("isLightAttack", true);
        yield return new WaitForSeconds(.1f);
        _lightAttack.SetActive(false); 
        yield return new WaitForSeconds(.1f);
        _lightAttack.SetActive(true);
        animator.SetInteger("ComboInt", 2);
        yield return new WaitForSeconds(.1f);
        _lightAttack.SetActive(false);
        yield return new WaitForSeconds(.1f);
        _lightAttack.SetActive(true);
        animator.SetInteger("ComboInt", 3);
        yield return new WaitForSeconds(.1f);
        _lightAttack.SetActive(false);
        animator.SetInteger("ComboInt", 0);
        animator.SetBool("isLightAttack", false);
        // endlag
        yield return new WaitForSeconds(.2f);
        // Returns player to speed
        _ActiveSpeed = _Speed;
        _isAttacking = false;
    } */

    public IEnumerator ComboAttack() {
        // Coroutine for the multi-hitting combo attack.
        if (!_isAttacking && _comboLink < 3) {

            // Prevents concurrent attack
            _isAttacking = true;
            
            // Slows player
            _ActiveSpeed = 6;
            _lastLightAttackTime = Time.time;

            // Plays animation
            _comboLink++;
            animator.SetBool("isLightAttack", true);
            animator.SetInteger("ComboInt", _comboLink);

            yield return new WaitForSeconds((float)0.10);

            _ActiveSpeed = 3;
            
            // Instantiates hitbox prefab
            GameObject lightAttack1 = Instantiate(_lightAttack, gameObject.transform.position, transform.rotation);
            lightAttack1.transform.rotation = transform.rotation;

            // Destroys the instance.
            Destroy(lightAttack1, 0.1f);

            yield return new WaitForSeconds((float)0.40);

            animator.SetInteger("ComboInt", 0);
            animator.SetBool("isLightAttack", false);

            // player returns to normal speed and can do next combo attack
            yield return new WaitForSeconds((float)0.40);
            _ActiveSpeed = _Speed;
            _isAttacking = false;
            if (_comboLink >= 3) {
                _comboLink = 0;
            }
        }
    }
    
    public void OnLightAttack() {
        // Initiates the combo attack.
        if (gameObject.GetComponent<PlayerController>()._isFrenzied && !_isAttacking) {

            // Starts the attack coroutine to carry out the attack's duration
            StartCoroutine(ComboAttack());

        }
    }

    IEnumerator DoSomething(float duration) {
        // Debug.Log("Before");

        // waits here
        yield return new WaitForSeconds(duration);

        // Debug.Log("After");
    }

    public IEnumerator HeavyAttack() {
        // Coroutine for the heavy attack.
        if (!_isAttacking) {
            // prevents player from attacking repeatedly.
            _ActiveSpeed = 6;
            _isAttacking = true;
            animator.SetBool("isHeavyAttack", true);

            // Delays
            yield return new WaitForSeconds((float)0.30);
            _ActiveSpeed = 3;
            // Instantiates hitbox prefab
            GameObject heavyAttack = Instantiate(_heavyAttack, gameObject.transform.position, transform.rotation);
            heavyAttack.SetActive(true);
            _lastLightAttackTime = Time.time;
            Rigidbody2D rigidbody = heavyAttack.GetComponent<Rigidbody2D>();

            // Activates the heavy attack's hitbox and keeps it up for 2 seconds, feel free to change
            _heavyAttack.SetActive(true);
            // yield return new WaitForSeconds(.1f);
            _heavyAttack.SetActive(false);
            
            animator.SetBool("isHeavyAttack", false);

            // Destroys the instance.
            Destroy(heavyAttack, 1);

            yield return new WaitForSeconds((float)0.30);
            _ActiveSpeed = _Speed;
            _isAttacking = false;
        }
    }

    public void OnHeavyAttack() {

        // Initiates the heavy attack.
        if (gameObject.GetComponent<PlayerController>()._isFrenzied && !_isAttacking) {
            
            // Starts the attack coroutine to carry out the attack's duration
            StartCoroutine(HeavyAttack());
            _comboLink = 0;
            
        }
    }
}
