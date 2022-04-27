using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public List<ParticleSystem> particleSystems;
    public static ParticleSystemManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddSystem(ParticleSystem ps, float delay)
    {
        ps.gameObject.transform.parent = transform;
        var em = ps.emission;
        var main = ps.main;
        em.rateOverTime = 0f;
        main.loop = false;
        StartCoroutine(ManageSystem(ps, delay));
    }

    private IEnumerator ManageSystem(ParticleSystem ps, float delay)
    {
        particleSystems.Add(ps);
        yield return new WaitForSeconds(delay);
        particleSystems.Remove(ps);
        Destroy(ps.gameObject);
    }
}
