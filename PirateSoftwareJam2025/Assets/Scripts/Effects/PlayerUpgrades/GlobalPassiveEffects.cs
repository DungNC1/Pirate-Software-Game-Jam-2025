using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class GlobalPassiveEffects : MonoBehaviour
{
    public static GlobalPassiveEffects Instance;
    public float EnnemiesSlowDebuffPercentage = 0f;
    public float EnnemiesPVDebuffPercentage = 0f;
    public float PlayerDamageBuffPercentage = 0f;
    public float PlayerDamageBuffPercentageIncrement = 0.05f;

    private bool GaloreActive = false;
    private bool GetGaloreBuff { get { return GaloreActive; } }
    private float GaloreChance = -1f;

    private bool GutsActive = false;
    private bool GetGutsBuff { get { return GutsActive; } }
    private float GutsChance = -1f;

    public UnityEvent<float> SlowDebuffChange = new UnityEvent<float>();
    public UnityEvent<float> PVDebuffChange = new UnityEvent<float>();
    public UnityEvent<float> DamageBuffChange = new UnityEvent<float>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);

        Instance = this;
    }

    public void UpdateSlowDebuff(float amount)
    {
        EnnemiesSlowDebuffPercentage += amount;
        EnnemiesSlowDebuffPercentage = Mathf.Clamp01(EnnemiesSlowDebuffPercentage);
        Debug.Log("Slow Debuff Updated: " + EnnemiesSlowDebuffPercentage); // Debug line
        SlowDebuffChange.Invoke(EnnemiesSlowDebuffPercentage);
    }

    public void UpdatePVDebuff(float amount)
    {
        EnnemiesPVDebuffPercentage += amount;
        EnnemiesSlowDebuffPercentage = Mathf.Clamp01(EnnemiesPVDebuffPercentage);
        PVDebuffChange.Invoke(EnnemiesPVDebuffPercentage);
    }

    public void UpdateDamageBuff(int value)
    {
        PlayerDamageBuffPercentage += PlayerDamageBuffPercentageIncrement;
        DamageBuffChange.Invoke(PlayerDamageBuffPercentage);
    }

    public void ActivateGuts(float percent)
    {
        GutsActive = true;
        GutsChance = percent;
    }

    public void ActivateGalore(float percent)
    {
        GaloreActive = true;
        GaloreChance = percent;
    }

    public bool RollGaloreChance()
    {
        float percent = UnityEngine.Random.Range(0, 1f);
        if (percent > GaloreChance)
            return false;
        return true;
    }

    public bool RollGutsChance()
    {
        float percent = UnityEngine.Random.Range(0, 1f);
        if (percent > GutsChance)
            return false;
        return true;
    }
}
