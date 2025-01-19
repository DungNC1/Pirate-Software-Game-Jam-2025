using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AmmoSelectorUI : MonoBehaviour
{
    public static AmmoSelectorUI Instance;
    [SerializeField] Transform AmmoUIHolder;
    Transform[] Ammos;
    [SerializeField] RectTransform Selector;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        Instance = this;

        Ammos = new RectTransform[AmmoUIHolder.childCount];

        for(int i = 0; i < AmmoUIHolder.childCount; i++)
        {
            Ammos[i] = AmmoUIHolder.GetChild(i);
        }
    }

    public void SetSelector(int index)
    {
        Selector.position = Ammos[index].position;
    }
}
