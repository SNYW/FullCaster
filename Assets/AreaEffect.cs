using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public int damage;
    public float duration;
    public float effectFrequency;
    public ContactFilter2D contactFilter;
    public Collider2D affectArea;
    private float remainingDuration;


    private void Start()
    {
        remainingDuration = duration;
        StartCoroutine(DoEffects());
    }

    private void Update()
    {
        remainingDuration -= Time.deltaTime;
        if (remainingDuration <= 0)
            Destroy(gameObject);
    }

    private IEnumerator DoEffects()
    {
        while (true)
        {
            var targets = new List<Collider2D>();
            Physics2D.OverlapCollider(affectArea, contactFilter, targets);
            foreach (var enemy in targets.Where(col => col.GetComponent<Enemy>() != null))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage, 1);
            }
            yield return new WaitForSeconds(effectFrequency);
        }
    }
}
