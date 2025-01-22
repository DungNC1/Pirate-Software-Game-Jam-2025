using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int MaxHealth;
    [SerializeField] private int currentHealth;
    public UnityEvent<Vector3> OnDamaged = new UnityEvent<Vector3>();

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        OnDamaged.Invoke(transform.position);

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
