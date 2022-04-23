
using UnityEngine;

[CreateAssetMenu]
public class AreaSpell : Spell
{

    public override bool Cast(int level)
    {
        switch (targetType)
        {
            case (TargetType.Self):
                return CastSelf(level);
            default:
                return false;
        }
    }

    private bool CastSelf(int level)
    {
        var mage = GameManager.Instance.playerMage;

        if (useCastAnim)
            mage.PlayCastAnim();

        Instantiate(tiers[level].projectile, mage.aoeAnchor.transform.position, Quaternion.identity, mage.transform);
        return true;
    }
}
