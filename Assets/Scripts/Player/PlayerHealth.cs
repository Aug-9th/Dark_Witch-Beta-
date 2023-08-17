using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Private Value
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int curHealth;
    [SerializeField] private bool wasDead = false;
    [SerializeField] private Animator animator;
    #endregion
    #region Public Value
    public HealthBar healthbar;
    public GameObject GameOverUI;
    public GameObject hitffect;
    #endregion
    #region Start and Update
    private void Start()
    {
        animator = GetComponent<Animator>();
        curHealth = maxHealth;
        healthbar.SetMaxValue(maxHealth);
    }

    public void Update()
    {
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        healthbar.SetValue(curHealth);
        if (curHealth <= 0)
            Die();
    }
    #endregion
    #region Hurt, Death
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        animator.SetTrigger("isHurt");
        Instantiate(hitffect, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        wasDead = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        animator.SetBool("wasDead", true);
        GetComponent<PlayerMoving>().Dead();
    }
    #endregion
    #region GameOver UI
    private void GameOver()
    {
        if (wasDead)
        {
            Time.timeScale = 0;
            GameOverUI.SetActive(true);
        }
    }
    #endregion
}
