using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    Slider XPSlider;

    private void Start()
    {
        TryGetComponent(out XPSlider);
        SetSliderMax();
        PlayerXP.instance.XPGained.AddListener(UpdateSlider);
        PlayerXP.instance.LevelGained.AddListener(SetSliderMax);
    }

    void SetSliderMax()
    {
        XPSlider.value = PlayerXP.instance.GetcurrentXP;
        XPSlider.maxValue = PlayerXP.instance.GetXPToNextLevel;
    }

    private void UpdateSlider(int value)
    {
        XPSlider.value = PlayerXP.instance.GetcurrentXP;
    }
}
