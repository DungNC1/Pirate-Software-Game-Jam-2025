using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        GlobalPassiveEffects.Instance.PVDebuffChange.AddListener(ChangeHealth);
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

    public void ChangeHealth(float percentage)
    {
        currentHealth = (int)(currentHealth * (1 + percentage));
        health = (int)(health * (1 + percentage));
    }
}
