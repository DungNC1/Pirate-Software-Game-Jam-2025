using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

    [SerializeField] private Volume v;
    [SerializeField] private Vignette vg;
    CinemachineImpulseSource source;

    private float biteBackRange = 1.5f;

    private void Start()
    {
        TryGetComponent(out source);
        currentHealth = MaxHealth;
        v = Camera.main.GetComponent<Volume>();
        v.profile.TryGet(out vg);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        OnDamagedPosition.Invoke(transform.position);
        OnDamaged.Invoke(damage);
        SetRedVignetteIntensity();
        source.GenerateImpulse();
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


    public void SetRedVignetteIntensity()
    {
        float maxIntensity = 0.65f;
        float newIntensity = (float)(MaxHealth - currentHealth) / MaxHealth * maxIntensity;
        newIntensity = Mathf.Clamp(newIntensity, 0, maxIntensity);
        vg.intensity.value = newIntensity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, biteBackRange);
    }
}
