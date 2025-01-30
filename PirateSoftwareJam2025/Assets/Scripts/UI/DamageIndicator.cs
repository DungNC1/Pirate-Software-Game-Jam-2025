using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    TextMeshPro Damagetext;

    private void Start()
    {
        Damagetext = GetComponentInChildren<TextMeshPro>();
        Destroy(gameObject, 0.5f);
    }
    public void InitDamage(int value)
    {
        Damagetext.text = value.ToString();
    }
}
