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
    public List<Spell> defaultSpells = new List<Spell>();
    public List<SecondarySpellEffect> secondaryEffects = new List<SecondarySpellEffect>();

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
        foreach (SpellSchool s in selectedSpellSchools)
        {
            foreach (Spell sp in s.spells)
            {
                if (sp != null)
                {
                    possibleSpells.Add(sp);
                }
                else
                {
                    Debug.LogError($"Null spell in SpellSchool ${s.name}");
                }
            }
        }
    }

    private void Update()
    {
        ManageSpellCooldowns();
    }

    private void ManageSpellCooldowns()
    {
        foreach (SpellInstance spell in activeSpells)
        {
            if (spell.remainingCooldown - Time.deltaTime <= 0)
            {
                spell.remainingCooldown = int.MaxValue;
                StartCoroutine(spell.Cast());
            }
            else
            {
                spell.remainingCooldown -= Time.deltaTime;
            }
        }

        foreach (SpellSlot slot in spellSlotParent.GetComponentsInChildren<SpellSlot>())
        {
            slot.UpdateCoolDown();
        }
    }

    public List<SpellInstance> GetSpellChoices()
    {
        var validSpells = new List<SpellInstance>();
        var maxSize = Mathf.Min(3, possibleSpells.Count);
        maxSize = maxSize == 0 ? defaultSpells.Count : maxSize;
        while (validSpells.Count < maxSize)
        {
            var newChoiceSpell = GetRandomSpell();
            if (!validSpells.Contains(newChoiceSpell) && !HasChoiceAlready(validSpells, newChoiceSpell))
            {
                validSpells.Add(newChoiceSpell);
            }
        }
        return validSpells;
    }

    private bool HasChoiceAlready(List<SpellInstance> validSpells, SpellInstance newChoiceSpell)
    {
        foreach (SpellInstance choiceSpell in validSpells)
        {
            if (choiceSpell.spell == newChoiceSpell.spell)
                return true;
        }
        return false;
    }

    public SpellInstance GetRandomSpell()
    {
        var chooseInstance = new SpellInstance();
        if (possibleSpells.Count > 0)
        {
            var spell = Utils.RandomFromList(possibleSpells);
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
            var spell = Utils.RandomFromList(defaultSpells);
            chooseInstance.Init(spell);
            return chooseInstance;
        }
    }

    public void AddSpell(Spell spell)
    {
        if (spell is EffectSpell)
        {
            spell.OnAdd();
        }
        else
        {
            if (HasSpell(spell, out var instance))
            {
                instance.IncrementLevel();
                if (instance.IsMaxed())
                {
                    possibleSpells.Remove(instance.spell);
                    instance.level = instance.spell.tiers.Length - 1;
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
    }

    private bool HasSpell(Spell spell, out SpellInstance instance)
    {
        instance = null;
        foreach (SpellInstance inst in activeSpells)
        {
            if (inst.spell == spell)
            {
                instance = inst;
                return true;
            }
        }

        return false;
    }

    public void TriggerGlobalCooldown()
    {
        //activeSpells.ForEach(spell => spell.remainingCooldown += 1);
    }
}
