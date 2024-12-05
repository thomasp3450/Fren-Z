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

    public ProgressData progressData;
    
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
        if (!gameObject.GetComponent<PlayerPause>()._pauseMenu.activeSelf && 
        !gameObject.GetComponent<PlayerGameOver>()._gameoverMenu.activeSelf && gameObject.GetComponent<PlayerController>()._amountOfBloodBombs > 0) {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire >= _timeBetweenShots || _lastFireTime == 0) {
                gameObject.GetComponent<PlayerController>()._amountOfBloodBombs--;
                gameObject.GetComponent<PlayerController>().progressData.SetProgressData(gameObject.GetComponent<PlayerController>().GetCurrentLevel(), gameObject.GetComponent<PlayerController>()._amountOfBloodBombs, gameObject.GetComponent<PlayerController>()._amountOfSyringes);
                gameObject.GetComponent<PlayerController>().SaveData();
                ThrowBloodBomb();  
                if (gameObject.GetComponent<PlayerController>()._comboLink > 0) {
                    gameObject.GetComponent<PlayerController>()._comboLink = 0;
                    gameObject.GetComponent<PlayerController>()._currentComboAttackCooldown += gameObject.GetComponent<PlayerController>().GetComboAttackCooldown();
                }
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
        }
    }
}
