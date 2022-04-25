using UnityEngine;

[CreateAssetMenu]
public class ProjectileSpell : Spell
{
    public bool tracking;

    public override bool Cast(int level)
    {
        switch (targetType)
        {
            case (TargetType.Closest):
                return CastClosest(level);
            default:
                return false;
        }
    }

    private bool CastClosest(int level)
    {
        var mage = GameManager.Instance.playerMage;
        mage.target = GameManager.Instance.GetClosestEnemy();

        if (mage.target != null)
        {
            if (useCastAnim)
                mage.PlayCastAnim();

            var proj = Instantiate(tiers[level].projectile,
                    mage.projectileAnchor.transform.position,
                    Quaternion.identity,
                    mage.transform)
                    .GetComponent<Projectile>();

            proj.Shoot(mage.target.transform.position, tracking);

            if (castEffect != null)
                Instantiate(castEffect, mage.projectileAnchor.transform.position, Quaternion.identity, mage.transform);

            return true;
        }

        return false;
    }
}