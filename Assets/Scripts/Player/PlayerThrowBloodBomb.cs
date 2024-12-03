using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowBloodBomb : MonoBehaviour {
    [SerializeField]
    private GameObject _bloodBombPrefab;

    [SerializeField]
    private float _throwingSpeed;

    [SerializeField]
    private Transform _throwOffset;


    [SerializeField]
    private float _timeBetweenShots;
    
    private float _lastFireTime;
    
    Animator animator;


    // Start is called before the first frame update
    void Start() {
      animator = GetComponent<Animator>();
    }   

    private void ThrowBloodBomb() {
        GameObject bloodBomb = Instantiate(_bloodBombPrefab, _throwOffset.position, transform.rotation);
        _lastFireTime = Time.time;
        Rigidbody2D rigidbody = bloodBomb.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _throwingSpeed * transform.up;
        Destroy(bloodBomb, 5);
    }



    public void OnThrow(InputAction.CallbackContext context) {
        if (!gameObject.GetComponent<PlayerPause>()._pauseMenu.activeSelf && gameObject.GetComponent<PlayerController>()._amountOfBloodBombs > 0) {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire >= _timeBetweenShots || _lastFireTime == 0) {
                gameObject.GetComponent<PlayerController>()._amountOfBloodBombs--;
                ThrowBloodBomb();  
                if (gameObject.GetComponent<PlayerController>()._comboLink > 0) {
                    gameObject.GetComponent<PlayerController>()._comboLink = 0;
                    gameObject.GetComponent<PlayerController>()._currentComboAttackCooldown += gameObject.GetComponent<PlayerController>().GetComboAttackCooldown();
                }
                // animator.SetBool("isThrowingBloodBomb", true);
            }else{
                // animator.SetBool("isThrowingBloodBomb", false);
            }
        }
    }

    public IEnumerator DetonateBloodBomb() {
        if (!gameObject.GetComponent<PlayerController>()._isAttacking) {
            gameObject.GetComponent<PlayerController>()._comboLink = 0;
            gameObject.GetComponent<PlayerController>()._isAttacking = true;
            yield return new WaitForSeconds(2);
            _bloodBombPrefab.SetActive(true);
            
            yield return new WaitForSeconds(.5f);
            _bloodBombPrefab.SetActive(false);
            gameObject.GetComponent<PlayerController>()._ActiveSpeed = gameObject.GetComponent<PlayerController>()._Speed;
            gameObject.GetComponent<PlayerController>()._isAttacking = false;
            // animator.SetBool("isThrowingBloodBomb", false);
        }
    }
}
