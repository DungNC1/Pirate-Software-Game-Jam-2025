using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
    public Image icon;
    [SerializeField] Button button;
    [SerializeField] Upgrade Upgrade;
    [SerializeField] UpgradeMenu Menu;

    public void InitUI(Upgrade upgrade)
    {
        Upgrade = upgrade;
        name.text = upgrade.Name;
        description.text = upgrade.Description;
        icon.sprite = upgrade.Icon;
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
