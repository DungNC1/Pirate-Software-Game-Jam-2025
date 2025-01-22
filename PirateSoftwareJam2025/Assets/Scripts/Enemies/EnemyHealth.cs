using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    private int currentHealth;
    AbstractEnnemy MainScriptRef;

    private void Awake()
    {
        TryGetComponent(out MainScriptRef);
    }

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            MainScriptRef.Die();
            Destroy(gameObject);
        }
    }
}
