using UnityEngine;

public class Spell : ScriptableObject
{
    public string spellName;
    public bool useCastAnim;
    public ProjectileSpellTier[] tiers;

    public enum TargetType
    {
        Closest,
        Random,
        Furthest,
        MostHP,
        LowestHP,
        Self
    }

    public TargetType targetType;

    public enum DamageType 
    {
        Default
    }

    public virtual bool Cast(int level) { return false; }
    public virtual void OnAdd() { }
}
