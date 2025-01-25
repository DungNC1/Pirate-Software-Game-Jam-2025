using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] List<Upgrade> m_UpgradeList;
    UpgradeUI[] m_UpgradeUIArray =  new UpgradeUI[4];
    Transform Container;

    private void Start()
    {
        Container = transform.GetChild(0);
        m_UpgradeUIArray = GetComponentsInChildren<UpgradeUI>();
        DisplayChoice();
    }

    void DisplayChoice()
    {
        Container.gameObject.SetActive(true);
        List<Upgrade> Upgrades = DraftUpgrade(4);
        foreach (UpgradeUI upgradeUI in m_UpgradeUIArray)
        {
            int randUpgrade = Random.Range(0, Upgrades.Count);
            upgradeUI.InitUI(m_UpgradeList[randUpgrade]);
            m_UpgradeList.RemoveAt(randUpgrade);

        }
    }

    List<Upgrade> DraftUpgrade(int nbr)
    {
        List<Upgrade> result = new List<Upgrade>();
        for(int i = 0; i < nbr; i++)
        {
            int choice = Random.Range(0, m_UpgradeList.Count);
            result.Add(m_UpgradeList[choice]);
            m_UpgradeList.RemoveAt(choice);
        }
        return result;
    }
}
