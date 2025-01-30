using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] TextMeshPro Damagetext;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    public void InitDamage(int value)
    {
        Damagetext.text = value.ToString();
    }
}
