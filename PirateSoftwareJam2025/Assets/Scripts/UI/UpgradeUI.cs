using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite Icon;
    Button button;
    Upgrade Upgrade;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }

    public void InitUI(Upgrade upgrade)
    {
        Upgrade = upgrade;
        Name = upgrade.Name;
        Description = upgrade.Description;
        Icon = upgrade.Icon;
        button.onClick.AddListener(Upgrade.ApplyUpgrade);
    }
}
