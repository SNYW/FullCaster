using UnityEngine;

[CreateAssetMenu]
public class EffectSpell : Spell
{
    public int gemAmount;

    public override void OnAdd()
    {
        GameManager.Instance.AddPlayerGems(gemAmount);
    }
}
