using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AddGemsSpell : EffectSpell
{
    public int gemAmount;

    public override void OnAdd()
    {
        GameManager.Instance.AddPlayerGems(gemAmount);
    }
}
