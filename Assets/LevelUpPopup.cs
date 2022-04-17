using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPopup : MonoBehaviour
{
    public SpellChoiceButton[] buttons;
    private void OnEnable()
    {
        GameManager.Instance.PauseGame();
        foreach (SpellChoiceButton button in buttons)
        {
            button.SetSpell(SpellManager.Instance.GetRandomSpells());
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumeGame();
    }
}
