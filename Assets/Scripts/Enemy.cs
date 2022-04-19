using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int baseHealth;
    public int currentHealth;
    public int attackDamage;
    public float moveSpeed;
    public float visionRange;
    public float attackRange;
    public int expValue;
    private EnemyState state;

    private enum EnemyState
    {
        Moving,
        Knockback,
        Attacking
    }

    private void Start()
    {
        state = EnemyState.Moving;
        currentHealth = baseHealth;    
    }

    void Update()
    {
        if (GameManager.Instance.playing )
        {
            ManageMove();
        }
        
    }

    private void ManageMove()
    {
        if (state == EnemyState.Moving)
        {
            var magePos = GameManager.Instance.playerMage.gameObject.transform.position;
            var dist = Utils.GetDistance(magePos, transform.position);
            if (dist > visionRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + Vector2.left, moveSpeed * Time.deltaTime);
            }
            else if (dist > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, magePos, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(int damage, float knockbackDist)
    {
        if(currentHealth - damage <= 0)
        {
            Die();
        }
        else
        {
            currentHealth -= damage;
        }
        StartCoroutine(HandleKnockback(transform.position - (GameManager.Instance.playerMage.transform.position-transform.position).normalized * 0.1f));
    }

    private IEnumerator HandleKnockback(Vector2 targetPos)
    {
        state = EnemyState.Knockback;
        while((Vector2)transform.position != targetPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed*8 * Time.deltaTime);
            yield return null;
        }
        state = EnemyState.Moving;
    }

    public void Die()
    {
        GameManager.Instance.Enemies.Remove(this);
        GameManager.Instance.AddExp(expValue);
        Destroy(this.gameObject);
    }
}
