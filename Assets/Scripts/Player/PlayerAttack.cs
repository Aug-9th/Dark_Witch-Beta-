using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Public Value
    public SkillsManager curSkill;
    public Animator animator;
    public GameObject hitEffect;
    public Transform shootingPoint;
    public LayerMask groundLayer;
    public Transform groundPoint;
    public HealthBar healthBar;
    public GameObject spellcd;
    #endregion
    #region Private Value
    [SerializeField] private int combo = 0;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool can_skill = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] private int m_damage = 10;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private int curMana;
    #endregion
    #region Start and Update
    private void Start()
    {
        animator = GetComponent<Animator>();
        curMana = maxMana;
        healthBar.SetMaxValue(maxMana);
    }

    private void Update()
    {
        curMana = Mathf.Clamp(curMana, 0, maxMana);
        healthBar.SetValue(curMana);
        Combo_onGround();
        UseSkill_1();
    }
    #endregion
    #region Combo
    private void Start_Combo()
    {
        isAttacking = false;
        if (combo < 3)
        {
            
            combo++;
        }
    }
    private void Finish_Combo()
    {
        isAttacking = false;
        combo = 0;
    }
    private void resetAttack()
    {
        isAttacking = false;
        combo = 0;
    }
    #endregion
    #region Attack inAir and onGround
    private void Combo_onGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Attack") && !isAttacking && isGrounded)
        {
            isAttacking = true;
            animator.SetTrigger("Attack" + combo);
        }
        if (!isGrounded && Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("attackair");
        }
    }
    #endregion
    #region Collision handling
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GainMana(10);
            Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
            collision.GetComponent<EnemyHealth>().TakeDamage(m_damage);
        }
        if (collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
        }
    }
    #endregion
    #region Skill
    private void UseSkill_1()
    {
        if (Input.GetMouseButton(1)  && !can_skill && curMana > 15)
        {
            spellcd.GetComponent<Spell_CoolDown>().Timer = curSkill.cooldownTime;
            UseMana(curSkill.manaCost);
            animator.SetTrigger("MagicBullet");
            can_skill = true;
            StartCoroutine(CoolDown());
        }
    }

    private void MakeSkill()
    {
        Instantiate(curSkill.Skill, shootingPoint.position, shootingPoint.rotation);
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(curSkill.cooldownTime);
        can_skill = false;
    }
    #endregion
    #region Manager Mana
    public void GainMana(int mana)
    {
        curMana += mana;
    }
    public void UseMana(int mana)
    {
        curMana -= mana;
    }
    #endregion
}
