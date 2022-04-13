using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellChoiceButton : MonoBehaviour
{
    public Spell spell;

    public void OnClick()
    {
        SpellManager.Instance.AddSpell(spell);
        PopupManager.Instance.CloseAll();
    }
}
