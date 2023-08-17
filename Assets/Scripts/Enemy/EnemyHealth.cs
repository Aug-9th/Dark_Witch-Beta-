using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    public HealthBar healthBar;

    public float knockBackForce = 100f;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetValue(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float dis = target.localPosition.x - transform.localPosition.x;
        if (dis > 0f)
        {
            rb.AddForce(Vector2.left * knockBackForce);
        }
        else
        {
            rb.AddForce(Vector2.right * knockBackForce);
        }
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        { 
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        animator.SetBool("Dead", true);
        Destroy(gameObject, 2f);
    }
}

