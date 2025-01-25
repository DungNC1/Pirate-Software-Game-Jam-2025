using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private int currentHealth;
    public UnityEvent<Vector3> OnDamagedPosition = new UnityEvent<Vector3>();
    public UnityEvent OnDamaged = new UnityEvent();

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        OnDamagedPosition.Invoke(transform.position);
        OnDamaged.Invoke();

        if(currentHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }

    public void BuffHealth(int Buff)
    {
        MaxHealth += Buff;
        Heal(Buff);
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
    }
}
