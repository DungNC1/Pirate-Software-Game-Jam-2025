using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] List<Upgrade> m_UpgradeList;
    [SerializeField] List<Upgrade> m_UpgradeSelected = new List<Upgrade>();
    [SerializeField] UpgradeUI[] m_UpgradeUIArray =  new UpgradeUI[4];
    Transform Container;

    private void Awake()
    {
        Container = transform.GetChild(0);
        m_UpgradeUIArray = GetComponentsInChildren<UpgradeUI>(true);
    }

    private void Start()
    {
        PlayerXP.instance.LevelGained.AddListener(DisplayChoice);
    }

    void DisplayChoice()
    {
        Container.gameObject.SetActive(true);
        m_UpgradeSelected = DraftUpgrade(4);

        List<Upgrade> selectedUpgrades = new List<Upgrade>();

        foreach (UpgradeUI upgradeUI in m_UpgradeUIArray)
        {
            if (m_UpgradeSelected.Count > 0)
            {
                int randUpgrade = Random.Range(0, m_UpgradeSelected.Count);
                Upgrade selectedUpgrade = m_UpgradeSelected[randUpgrade];

                selectedUpgrades.Add(selectedUpgrade);

                upgradeUI.InitUI(selectedUpgrade);

                m_UpgradeSelected.RemoveAt(randUpgrade);
            }
        }

        foreach (Upgrade upgrade in selectedUpgrades)
        {
            m_UpgradeList.Remove(upgrade);
        }

        Time.timeScale = 0f;
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

    private void ResumeGame()
    {
        Container.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnChooseUpgrade(Upgrade upgrade)
    {
        m_UpgradeSelected.Remove(upgrade);
        m_UpgradeList.AddRange(m_UpgradeSelected);
        m_UpgradeSelected.Clear();
        ResumeGame();
    }
}
