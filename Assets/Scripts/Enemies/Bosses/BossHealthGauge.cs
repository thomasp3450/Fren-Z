using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthGauge : MonoBehaviour {
    [SerializeField] public GameObject _healthBar;
    [SerializeField] public GameObject _healthBackground;

    private void Update() {
        if (gameObject.GetComponent<PlayerAwarenessController>().AwareOfPlayer) {
            _healthBar.SetActive(true);
            _healthBackground.SetActive(true);
            _healthBar.GetComponent<Image>().fillAmount = gameObject.GetComponent<HealthController>().RemainingHealthPercentage;
        }
    }

    public void OnDeath() {
        if (gameObject != null) {
            _healthBackground.SetActive(false);
            _healthBar.SetActive(false);
        }
    }
}
