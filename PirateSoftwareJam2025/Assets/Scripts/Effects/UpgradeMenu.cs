using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] List<Upgrade> m_UpgradeList;

    void DisplayChoice()
    {
        Upgrade[] Upgrades = DraftUpgrade(3);
    }

    Upgrade[] DraftUpgrade(int nbr)
    {
        Upgrade[] result = new Upgrade[nbr];
        for(int i = 0; i < nbr; i++)
        {
            int choice = Random.Range(0, m_UpgradeList.Count);
            result[i] = m_UpgradeList[choice];
            m_UpgradeList.RemoveAt(choice);
        }
        return result;
    }
}
