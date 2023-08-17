using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SkillsManager curskill;
    private GameObject hitEffect;
    private void Start()
    {
        Destroy(gameObject, curskill.timeLife);
        float playerScale = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
        if (playerScale < 0)
        {
            Flip();
        }
        hitEffect = GameObject.Find("Player").GetComponent<PlayerAttack>().hitEffect;
    }

    private void Update()
    {
        float moveAmount = curskill.speed * Time.deltaTime;
        transform.Translate(Vector3.right * moveAmount * Mathf.Sign(transform.localScale.x));
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(curskill.damage);
                Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground") || collision.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
            if (collision.CompareTag("EnemyBullet"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
