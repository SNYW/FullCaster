using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public int damage;
    public float duration;
    public float effectFrequency;
    public LayerMask layerMask;
    public SphereCollider effectArea;
    private float remainingDuration;
    public List<Collider> targets;

    private void Start()
    {
        remainingDuration = duration;
        StartCoroutine(DoEffects());
    }

    private void Update()
    {
        remainingDuration -= Time.deltaTime;
        if (remainingDuration <= 0)
            OnEnd();
    }

    private void OnEnd()
    {
        var particles = GetComponentsInChildren<ParticleSystem>();
        if (particles != null)
            particles.ToList().ForEach(sys => HandleParticles(sys));
        Destroy(gameObject,3);
        gameObject.SetActive(false);
    }

    private void HandleParticles(ParticleSystem sys)
    {
        var em = sys.emission;
        em.rateOverTime = 0f;
        sys.transform.parent = null;
        Destroy(sys.gameObject, 3);
    }

    private IEnumerator DoEffects()
    {
        while (true)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, effectArea.radius, layerMask);
          
            foreach (var enemy in targets.Where(col => col.GetComponent<Enemy>() != null))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage, 1);
            }
            yield return new WaitForSeconds(effectFrequency);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectArea.radius);
    }
}
