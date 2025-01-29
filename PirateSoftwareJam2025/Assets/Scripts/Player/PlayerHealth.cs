using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int MaxHealth;
    public int GetMaxHealth { get { return MaxHealth; } }
    [SerializeField] private int currentHealth;
    public int GetCurrentHealth { get { return currentHealth; } }
    [SerializeField] DamageIndicator indicator;
    public UnityEvent<Vector3> OnDamagedPosition = new UnityEvent<Vector3>();
    public UnityEvent<int> OnDamaged = new UnityEvent<int>();
    public UnityEvent OnDie = new UnityEvent();

    private float biteBackRange = 1.5f;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        OnDamagedPosition.Invoke(transform.position);
        OnDamaged.Invoke(damage);

        if (currentHealth <= 0)
        {
            OnDie.Invoke();
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

    void SpawnIndicator(int damage)
    {
        DamageIndicator TempIndicator = Instantiate(indicator, transform.position, Quaternion.identity);
        TempIndicator.InitDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, biteBackRange);
    }
}
