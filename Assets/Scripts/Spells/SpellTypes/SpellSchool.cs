using UnityEngine;

[CreateAssetMenu]
public class SpellSchool : ScriptableObject
{
    public string schoolName;
    public Spell[] spells;

    public virtual Spell GetRandomSpell() { return spells[Random.Range(0, spells.Length - 1)]; }
}
