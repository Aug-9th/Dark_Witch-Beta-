using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform target;
    public float moveSpeed;
    public int damage;


    private bool isFacingRight = true;
    public float within_range;

    public bool can_attack;
    public LayerMask enemyLayer;

    public Rigidbody2D rb;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        target = GameObject.Find("Player").transform;
        if (Vector2.Distance(transform.position, target.position) < 0.7f)
        {
            Attack();
            return;
        }
            

        float dist = Vector3.Distance(target.position, transform.position);

        if (dist <= within_range)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            Flip();
            if (animator)
            {
                animator.SetBool("Run", true);
            }
        }
        else
        {
            if (animator)
            {
                animator.SetBool("Run", false);
            }
        }
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void Flip()
    {
        float dis = target.localPosition.x - transform.localPosition.x;
        if ((isFacingRight && dis < 0f) || (!isFacingRight && dis > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    void start_attack()
    {
        moveSpeed = 0;
    }
    void end_attack()
    {
        moveSpeed = 1;
    }
}
