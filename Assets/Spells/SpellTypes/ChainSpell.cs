using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ChainSpell : Spell
{
    public SecondaryEffects chainTarget;

    public enum SecondaryEffects
    {
        IceShards
    }

    public override bool Cast(int level)
    {
        var dartList = SpellManager.Instance.secondaryEffects.Where(effect => effect.chainTarget == chainTarget).ToList();

        foreach (var secondaryEffect in dartList)
        {
            var dart = secondaryEffect as DartEffect;
            if(dart.enemy != null)
            {
                dart.enemy.TakeDamage(10 + 5 * level, 0);
                dart.OnShatter();
            }
        }

        dartList.ForEach(s => SpellManager.Instance.secondaryEffects.Remove(s));
        return true;
    }
}
