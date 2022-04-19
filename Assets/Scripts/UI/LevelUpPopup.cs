using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPopup : MonoBehaviour
{
    public Transform buttonAnchor;
    public GameObject choiceButton;

    private void OnEnable()
    {
        GameManager.Instance.PauseGame();
        var validSpells = SpellManager.Instance.GetSpellChoices();
        foreach(SpellInstance spellInstance in validSpells)
        {
            var button = Instantiate(choiceButton, buttonAnchor).GetComponent<SpellChoiceButton>();
            button.SetSpell(spellInstance);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumeGame();
        foreach(Transform t in buttonAnchor)
        {
            if (t.GetComponent<SpellChoiceButton>() != null)
                Destroy(t.gameObject);
        }
    }
}
