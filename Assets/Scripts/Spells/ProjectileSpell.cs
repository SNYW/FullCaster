using UnityEngine;

[CreateAssetMenu]
public class ProjectileSpell : Spell
{
    public GameObject projectilePrefab;
    public bool tracking;

    public override void Cast()
    {
        var mage = GameManager.Instance.playerMage;
        var proj = Instantiate(projectilePrefab, mage.transform.position, Quaternion.identity, mage.transform);

        switch (targetType)
        {
            case (TargetType.Closest):
                proj.GetComponent<Projectile>().Shoot(GameManager.Instance.GetClosestEnemy().transform.position, tracking);
                break;
            default:
                break;
        }

    }
}
