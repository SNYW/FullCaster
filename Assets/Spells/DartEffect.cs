using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DartEffect : SecondarySpellEffect
{
    protected override void OnInit()
    {
        base.OnInit();
        transform.position = anchorPos;
        transform.parent = enemy.transform;
    }

    public void OnShatter()
    {
        var eff = Instantiate(particleEffect, transform.position, Quaternion.identity).GetComponentsInChildren<ParticleSystem>();
        eff.ToList().ForEach(ps => ParticleSystemManager.Instance.AddSystem(ps, 2));
        Destroy(gameObject);
    }
}
