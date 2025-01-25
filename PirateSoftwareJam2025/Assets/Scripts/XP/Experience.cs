using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Experience : MonoBehaviour
{
    private int xpValue;
    public void Init(int amount)
    {
        xpValue = amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerXP.instance.GainXP(xpValue);
        Destroy(gameObject);
    }
}
