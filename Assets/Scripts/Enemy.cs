using System.Collections;
using System.Collections.Generic;
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
    public EnemyState state;
    public Vector3 targetPos;
    public List<SecondarySpellEffect> secondarySpellEffects = new List<SecondarySpellEffect>();
    public List<Transform> targetTransforms;
    private NavMeshAgent agent;

    public enum EnemyState
    {
        Moving,
        Knockback,
        Attacking
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(0.5f, 1.3f);
        state = EnemyState.Moving;
        currentHealth = baseHealth;
    }

    void Update()
    {
        if (GameManager.Instance.playing)
        {
            ManageMove();
        }

    }

    private void ManageMove()
    {
        if (state == EnemyState.Moving)
        {
            var magePos = GameManager.Instance.playerMage.gameObject.transform.position;
            agent.destination = new Vector3(magePos.x + attackRange, magePos.y, magePos.z);

        }
    }

    public void TakeDamage(int damage, float knockbackDist)
    {
        if (currentHealth - damage <= 0)
        {
            Die();
        }
        else
        {
            currentHealth -= damage;
        }
        targetPos = new Vector3(transform.position.x + knockbackDist, transform.position.y, transform.position.z);
        if(state != EnemyState.Knockback)
            StartCoroutine(HandleKnockback(targetPos));
        HPBar.UpdateHealthBar(currentHealth, baseHealth);
    }

    private IEnumerator HandleKnockback(Vector3 targetPos)
    {
        if (agent.isOnNavMesh)
        {
            state = EnemyState.Knockback;
            var baseSpeed = agent.speed;
            while (transform.position.x < targetPos.x-0.2f)
            {
                agent.SetDestination(targetPos);
                agent.speed = 10000;
                agent.acceleration = 10000;
                yield return null;
            }
            agent.speed = baseSpeed;
            state = EnemyState.Moving;
        }

    }

    public void Die()
    {
        secondarySpellEffects.ForEach(s => SpellManager.Instance.secondaryEffects.Remove(s));
        GameManager.Instance.Enemies.Remove(this);
        GameManager.Instance.AddExp(expValue);
        Destroy(this.gameObject);
    }
}
