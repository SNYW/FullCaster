using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;
    public float moveSpeed;
    public int damage;

    public void Shoot(Vector2 target, bool tracking)
    {
        if (!tracking)
        {
            dir = target - (Vector2)transform.position;
            StartCoroutine(FlyTowards());
        }
    }

    private IEnumerator FlyTowards()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dir * moveSpeed, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage, 1);
            OnHit();
        }
    }

    private void OnHit()
    {
        Destroy(gameObject);
    }
}
