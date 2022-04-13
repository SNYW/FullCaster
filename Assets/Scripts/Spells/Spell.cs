using UnityEngine;

public class Spell : ScriptableObject
{
    public float damage;
    public float coolDown;
    public float remainingCooldown;

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

    public virtual void Cast() { }
}
