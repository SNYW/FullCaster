using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public List<SpellInstance> activeSpells = new List<SpellInstance>();
    public static SpellManager Instance;
    public SpellSchool[] selectedSpellSchools;
    public GameObject spellSlotPrefab;
    public Transform spellSlotParent;
    public List<Spell> possibleSpells = new List<Spell>();

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
        PopulatePossibleSpells();
    }

    private void PopulatePossibleSpells()
    {
        foreach(SpellSchool s in selectedSpellSchools)
        {
            foreach(Spell sp in s.spells)
            {
                possibleSpells.Add(sp);
            }
        }
    }

    private void Update()
    {
        foreach(SpellInstance spell in activeSpells)
        {
            if(spell.remainingCooldown - Time.deltaTime <= 0)
            {
                spell.remainingCooldown = 0;
                spell.Cast();
            }
            else
            {
                spell.remainingCooldown -= Time.deltaTime;
            }
        }

        foreach(SpellSlot slot in spellSlotParent.GetComponentsInChildren<SpellSlot>())
        {
            slot.UpdateCoolDown();
        }
    }

    public SpellInstance GetRandomSpells()
    {
        if (possibleSpells.Count > 0)
        {
            var spell = possibleSpells[Random.Range(0, possibleSpells.Count - 1)];
            var chooseInstance = new SpellInstance();
            chooseInstance.Init(spell);

            if (HasSpell(spell, out var instance))
            {
                chooseInstance.level = instance.level + 1;
                return chooseInstance;
            }
            else
            {
                return chooseInstance;
            }
        }
        else
        {
            Debug.LogError("No possible spell choices");
            return null;
        }
        
    }

    public void AddSpell(Spell spell)
    {
        if (HasSpell(spell, out var instance))
        {
            instance.level++;
            if (instance.level >= instance.spell.tiers.Length - 1) 
            {
                possibleSpells.Remove(instance.spell);
                instance.level = instance.spell.tiers.Length-1;
            } 
        }
        else
        {
            var spellInstance = new SpellInstance();
            spellInstance.Init(spell);
            activeSpells.Add(spellInstance);
            var spellSlot = Instantiate(spellSlotPrefab, spellSlotParent).GetComponent<SpellSlot>();
            spellSlot.SetSpell(spell, spellInstance);
        }
    }

    private bool HasSpell(Spell spell, out SpellInstance instance)
    {
        instance = null;
        foreach(SpellInstance inst in activeSpells)
        {
            if(inst.spell == spell)
            {
                instance = inst;
                return true;
            }
        }

        return false;
    }
}
