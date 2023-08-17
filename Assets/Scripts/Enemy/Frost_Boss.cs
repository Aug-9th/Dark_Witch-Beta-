using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost_Boss : MonoBehaviour
{
    private Transform Target;
    private Animator animator;
    private Rigidbody2D rb;
    
    private bool isFacingRight = true;
    private bool isAttacking = false;
    private bool isReady = true;

    [SerializeField] private float Speed = 2f;
    [SerializeField] private float currSpeed;
    [SerializeField] private int damage = 33;
    [SerializeField] private int knockforce = 0;

    public Transform attackPoint1;
    public Transform attackPoint2;
    public Transform attackPoint3;
    public Transform attackPoint4;
    public Transform attackPoint5;
    public GameObject frostBullet;

    public GameObject GameUI;
    public GameObject WinUI;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currSpeed = Speed;
    }

    private void Update()
    {
        FindPlayer();
        if (Time.time > 15f)
        {
            PowerUp();
        }
        if (isReady)
        {
            StartCoroutine(AttackFrost());
        }
    }
    // Enemy Moving and Find Player
    private void FindPlayer()
    {
        Target = GameObject.Find("Player").transform;
        if (Vector2.Distance(transform.position, Target.position) < 2f)
        {
            if (!isAttacking)
            {
                Attack();
            }
        }
        float distance = Vector3.Distance(Target.position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, Target.position, currSpeed * Time.deltaTime);
        Flip();
    }
    // Attack
    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("Attack", true);
    }

    private void Flip()
    {
        float distance = Target.localPosition.x - transform.localPosition.x;
        if ((!isFacingRight && distance < 0f) || (isFacingRight && distance > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    // Fixed Attack
    private void start_attack()
    {
        currSpeed = 0;
    }

    private void end_start()
    {
        currSpeed = Speed;
        animator.SetBool("Attack", false);
        isAttacking = false;
    }
    // Enemy Power Up
    private void PowerUp()
    {
        Speed = 3f;
    }

    private IEnumerator AttackFrost()
    {
        isReady = false;
        yield return new WaitForSeconds(6);
        Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Instantiate(frostBullet, attackPoint1.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Instantiate(frostBullet, attackPoint2.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Instantiate(frostBullet, attackPoint3.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Instantiate(frostBullet, attackPoint4.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Instantiate(frostBullet, attackPoint5.position, Quaternion.AngleAxis(angle, Vector3.forward));

        isReady = true;
    }
    // Handle Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            float distance = collision.transform.localPosition.x - transform.localPosition.x;
            collision.GetComponent<PlayerMoving>().knockBack(knockforce, distance);
        }
    }
    public void Win_Game()
    {
        GameUI.SetActive(false);
        WinUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
