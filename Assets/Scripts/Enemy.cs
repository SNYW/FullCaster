using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int baseHealth;
    public int currentHealth;
    public EnemyHPBar HPBar;
    public int attackDamage;
    public float moveSpeed;
    public float visionRange;
    public float attackRange;
    public int expValue;
    private EnemyState state;
    private NavMeshAgent agent;

    private enum EnemyState
    {
        Moving,
        Knockback,
        Attacking
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
            agent.destination = magePos;
            
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
        HPBar.UpdateHealthBar(currentHealth, baseHealth);
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
