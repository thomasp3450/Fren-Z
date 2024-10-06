using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public UnityEvent HealthChanged;

    public UnityEvent Damaged;

    public UnityEvent Died;

    [SerializeField]
    private float _maximumHealth;

    [SerializeField]
    private float _currentHealth;

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvincible { get; set; }

    public float getHealth(){
        return _currentHealth;
    }

    public void TakeDamage(float amount)
    {
        if (IsInvincible == true || _currentHealth == 0)
        {
            return;
        }

        _currentHealth -= amount;
        
        HealthChanged.Invoke();

        ClampCurrentHealth();

        if (_currentHealth == 0)
        {
            Died.Invoke();
        }
        else
        {
            Damaged.Invoke();
        }
    }

    public void AddHealth(float amount)
    {
        _currentHealth += amount;
        ClampCurrentHealth();

        HealthChanged.Invoke();
    }

    public void SetHealth(float amount)
    {
        _currentHealth = amount;
        ClampCurrentHealth();
    }

    private void ClampCurrentHealth()
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
    }
}