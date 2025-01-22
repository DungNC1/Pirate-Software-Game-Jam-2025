using UnityEngine;

public class Upgrade : ScriptableObject
{
    public string Name;
    public virtual void ApplyUpgrade() { }
}
