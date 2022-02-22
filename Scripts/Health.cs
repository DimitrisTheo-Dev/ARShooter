using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _currentHealth;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public event Action<float> OnHealthChanged;
    public event Action OnDied;

    private void OnEnable()
    {
        _currentHealth = maxHealth;
    }

   
    public void ChangeHealth(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0.0f, maxHealth);

        float currentHealthPercent = _currentHealth / maxHealth;
        
        if(OnHealthChanged != null)
            OnHealthChanged(currentHealthPercent);

        if (_currentHealth <= 0.0f && OnDied != null)
            OnDied();
    }

    public void ApplyDamage(float amount)
    {
        ChangeHealth(-amount);
    }

}