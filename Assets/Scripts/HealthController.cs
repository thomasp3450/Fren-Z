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

    public bool _isInvincible;

    public float getHealth(){
        return _currentHealth;
    }

    public void TakeDamage(float amount)
    {
        // Enemy is invincible or their health is 0 => return statement/function does not activate
        if (_isInvincible == true || _currentHealth == 0)
        {
            return;
        }

        // Causes the invocation of the health-related events declared in the components.

        if (!_isInvincible) _currentHealth -= amount;
        
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
        // Heals
        _currentHealth += amount;
        ClampCurrentHealth();

        HealthChanged.Invoke();
    }

    public void SetHealth(float amount)
    {
        // Sets health to defined value
        _currentHealth = amount;
        ClampCurrentHealth();
    }

    public void InitIFrames() {
        // Makes entity invincible
        _isInvincible = true;
    }

    public void ExitIFrames() {
        // Makes entity not invincible.
        _isInvincible = false;
    }

    private void ClampCurrentHealth() {
        // Makes health a workable value.
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maximumHealth);
    }
}