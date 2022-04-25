using System.Collections;
using UnityEngine;

public class SpellInstance
{
    public int level;
    public float coolDown;
    public float remainingCooldown;
    public int amountToCast;
    public float castDelay;
    public Spell spell;

    public void Init(Spell spell)
    {
        level = 0;
        coolDown = spell.tiers[0].coolDown;
        amountToCast = spell.tiers[0].amount;
        castDelay = spell.tiers[0].delay;
        this.spell = spell;
    }

    public IEnumerator Cast()
    {
        SpellManager.Instance.TriggerGlobalCooldown();

        while (amountToCast > 0)
        {
            if (spell.Cast(level))
            {
                amountToCast--;
            }
            yield return new WaitForSeconds(castDelay);
        }

        remainingCooldown = spell.tiers[level].coolDown;
        amountToCast = spell.tiers[level].amount;
        castDelay = spell.tiers[level].delay;
    }

    public bool IsMaxed()
    {
        return level >= spell.tiers.Length - 1;
    }

    public void IncrementLevel()
    {
        level++;
        amountToCast = spell.tiers[level].amount;
        castDelay = spell.tiers[level].delay;
    }
}
