using System.Collections;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 dir;
    public float moveSpeed;
    public int damage;
    public float knockback;
    public float shakeAmount;
    public float shakeLenth;
    public float shakeDamp;
    public GameObject impactPrefab;
    public SecondarySpellEffect secondaryEffect;

    public void Shoot(Vector3 target, bool tracking)
    {
        if (!tracking)
        {
            dir = target - transform.position;
            StartCoroutine(FlyTowards());
        }
    }

    private IEnumerator FlyTowards()
    {
        while (true)
        {
            var pos = new Vector3(transform.position.x, transform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, pos + dir * moveSpeed, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            if(secondaryEffect != null)
            {
                var secondary = Instantiate(secondaryEffect, transform.position, Quaternion.identity);
                secondary.Init(enemy, collision.GetContact(0).point);
            }
            enemy.TakeDamage(damage, knockback);

            CameraShake.Instance.AddShakeDuration(shakeLenth, shakeAmount, shakeDamp);
        }

        OnHit();
    }

    private void OnHit()
    {
        var particles = GetComponentsInChildren<ParticleSystem>();
        if (particles != null)
            particles.ToList().ForEach(ps => ParticleSystemManager.Instance.AddSystem(ps, 2));

        var impact = Instantiate(impactPrefab, transform.position, Quaternion.identity, ParticleSystemManager.Instance.transform);
        impact.GetComponentsInChildren<ParticleSystem>()
            .ToList()
            .ForEach(ps => ParticleSystemManager.Instance.AddSystem(ps, 2));

        Destroy(gameObject);
    }
}
