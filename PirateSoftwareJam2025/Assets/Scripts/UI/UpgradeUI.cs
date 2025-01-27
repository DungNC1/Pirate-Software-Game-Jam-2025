using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite Icon;
    [SerializeField] Button button;
    [SerializeField] Upgrade Upgrade;
    [SerializeField] UpgradeMenu Menu;

    public void InitUI(Upgrade upgrade)
    {
        Upgrade = upgrade;
        Name = upgrade.Name;
        Description = upgrade.Description;
        Icon = upgrade.Icon;
        button.onClick.AddListener(Upgrade.ApplyUpgrade);
        int index = transform.GetSiblingIndex();
        button.onClick.AddListener(() => Menu.OnChooseUpgrade(Upgrade));
        button.onClick.AddListener(detachListener);
    }


    void detachListener()
    {
        button.onClick.RemoveAllListeners();
    }
}
