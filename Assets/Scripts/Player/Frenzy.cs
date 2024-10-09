using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to powerup prefab.

// Requires the game object (in this case, the power-up) has a 2D collider component.
[RequireComponent(typeof(Collider))]
public class Frenzy : MonoBehaviour {

    [SerializeField]
    private float _speedIncreaseAmount = 20;
    [SerializeField]
    private float _powerMultiplier = (float)1.2;
    [SerializeField]
    private float _powerupDuration = 3;
    [SerializeField]
    private GameObject _artToDisable = null;
    private Collider _collider;

    private void Awake() {
        _collider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        // If Player
        if (player != null) {
            // Player becomes frenzied
            StartCoroutine(PowerupSequence(player));
        }
    }
    public IEnumerator PowerupSequence(PlayerController player) {
        // disable the powerup object
        _collider.enabled = false;
        _artToDisable.SetActive(false);
        // activates the powerup
        ActivateFrenzy(player);
        // co routine stops until the powerups duration has passed
        yield return new WaitForSeconds(_powerupDuration);
        // deactivate
        DeactivateFrenzy(player);
        Destroy(gameObject);
    }

    private void ActivateFrenzy(PlayerController player) {
        player.SetPlayerDamageMultiplier(_powerMultiplier);
    }

    private void DeactivateFrenzy(PlayerController player) {
        player.ResetPlayerPower();
    }
}
