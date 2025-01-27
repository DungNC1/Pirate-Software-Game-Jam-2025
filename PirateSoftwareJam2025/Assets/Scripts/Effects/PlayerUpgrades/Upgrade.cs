using UnityEngine;

public class Upgrade : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public virtual void ApplyUpgrade() { }
}
