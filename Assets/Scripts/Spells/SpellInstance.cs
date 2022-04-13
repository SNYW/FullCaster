using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInstance
{
    public float coolDown;
    public float remainingCooldown;
    private Spell spell;

    public void Init(Spell spell)
    {
        coolDown = spell.coolDown;
        remainingCooldown = spell.coolDown;
        this.spell = spell;
    }

    public void Cast()
    {
        spell.Cast();
    }
}
