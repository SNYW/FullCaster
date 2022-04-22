using System.Collections;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 dir;
    public float moveSpeed;
    public int damage;
    public GameObject impactPrefab;

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
            var pos = new Vector3(transform.position.x, transform.position.y + 10);
            transform.position = Vector3.MoveTowards(transform.position, pos + dir * moveSpeed, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage, 1);
            var particles = GetComponentsInChildren<ParticleSystem>();

            OnHit();
        }
    }

    private void OnHit()
    {
        var impact = Instantiate(impactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 3);
        Destroy(gameObject, 3);
        gameObject.SetActive(false);
    }
}
