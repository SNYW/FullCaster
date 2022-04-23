using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public int level;
    public TMP_Text spellNameText;
    public Image cooldownIndicator;
    public Spell spell;
    public SpellInstance instance;

    public void SetSpell(Spell spell, SpellInstance instance)
    {
        level = 0;
        spellNameText.text = spell.spellName;
        this.spell = spell;
        this.instance = instance;
    }

    public void LevelUp(int level)
    {
        this.level = level;
    }

    internal void UpdateCoolDown()
    {
        cooldownIndicator.fillAmount = instance.remainingCooldown / instance.coolDown;
    }
}
