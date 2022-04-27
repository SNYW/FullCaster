using System.Linq;
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

            proj.Shoot(Utils.RandomFromList(mage.target.targetTransforms).position, tracking);
            
            if (castEffect != null)
            {
               var eff = Instantiate(castEffect, mage.projectileAnchor.transform.position, Quaternion.identity, ParticleSystemManager.Instance.transform);
                eff.GetComponentsInChildren<ParticleSystem>()
                .ToList()
                .ForEach(ps => ParticleSystemManager.Instance.AddSystem(ps, 2));
            }
                

            return true;
        }

        return false;
    }
}