using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public List<SpellInstance> activeSpells = new List<SpellInstance>();
    public static SpellManager Instance;
    public Spell testspell;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        foreach(SpellInstance spell in activeSpells)
        {
            if(spell.remainingCooldown - Time.deltaTime <= 0)
            {
                spell.Cast();
                spell.remainingCooldown = spell.coolDown;
            }
            else
            {
                spell.remainingCooldown -= Time.deltaTime;
            }
        }
    }

    public Spell GetRandomSpells()
    {
        return testspell;
    }

    public void AddSpell(Spell spell)
    {
        var spellInstance = new SpellInstance();
        spellInstance.Init(spell);
        activeSpells.Add(spellInstance);
    }

}
