using TMPro;
using UnityEngine;

public class SpellChoiceButton : MonoBehaviour
{
    public TMP_Text spellNameText;
    public TMP_Text description;
    public Spell spell;

    public void OnClick()
    {
        SpellManager.Instance.AddSpell(spell);
        PopupManager.Instance.CloseAll();
    }

    public void SetSpell(SpellInstance spellInstance)
    {
        var tier = spellInstance.spell.tiers[spellInstance.level];
        spellNameText.text = tier.tierName;
        description.text = tier.description;
        this.spell = spellInstance.spell;
    }
}
