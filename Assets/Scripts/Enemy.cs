using System;
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

    private void Start()
    {
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

    public void TakeDamage(int damage)
    {
        if(currentHealth - damage <= 0)
        {
            Die();
        }
        else
        {
            currentHealth -= damage;
        }
    }

    public void Die()
    {
        GameManager.Instance.Enemies.Remove(this);
        GameManager.Instance.AddExp(expValue);
        Destroy(this.gameObject);
    }
}
