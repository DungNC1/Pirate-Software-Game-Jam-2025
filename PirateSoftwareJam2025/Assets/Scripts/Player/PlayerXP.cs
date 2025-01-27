using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    public static PlayerXP instance;
    [SerializeField] AnimationCurve XPRequiredPerLevel;
    [SerializeField] int currentXP = 0;
    [SerializeField] int currentLevel = 1;
    [SerializeField] int XPToNextLevel = 0;
    public UnityEvent LevelGained = new UnityEvent();

    private void Awake()
    {
        if(instance != null && instance != this)
            Destroy(this);

        instance = this;

        XPToNextLevel = (int)XPRequiredPerLevel.Evaluate(currentLevel);
    }

    public void GainXP(int xp)
    {
        currentXP += xp;
        CheckXPCount();
    }

    void CheckXPCount()
    {
        if (XPToNextLevel > currentXP)
            return;
        
        currentLevel++;
        currentXP -= XPToNextLevel;
        XPToNextLevel = (int)XPRequiredPerLevel.Evaluate(currentLevel);
        LevelGained.Invoke();
    }
}