using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    #region Value
    public LayerMask groundLayer;
    public Transform groundCheck;
    private Rigidbody2D rb;
    private Animator m_animator;

    private bool isDashing = false;
    [SerializeField] private float dashingCooldown = 1.5f;
    [SerializeField] private float dashingPower = 30f;
    [SerializeField] private float dashingTime = 0.08f;
    private bool canDash = true;

    private int extraJump;
    [SerializeField] private int extraJumpValue = 1;
    [SerializeField] private float jumpForce = 7f;
    private bool isGrounded;

    [SerializeField] public float moveSpeed = 6f;
    private bool isFacingRight = true;
    public bool isDead;
    #endregion
    #region Start and Update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        extraJump = extraJumpValue;
        isDead = false;
    }
    private void Update()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (isDashing)
        {
            return;
        }
        HandleMovement();
        HandleJump();
        HandleDash();
    }
    #endregion
    #region Handle Move
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            Flip();
        }
        m_animator.SetBool("IsRunning", Mathf.Abs(moveInput) > 0);
    }
    #endregion
    #region Handle Dash
    private void HandleDash()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(Dash());
            m_animator.SetBool("dash", true);
        }
    }
    /*
    private void start_dash()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyBullet"), true);
    }

    private void end_dash()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyBullet"), false);
    }
    */
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        m_animator.SetBool("dash", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    #endregion
    #region Handle Jump and Animator
    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded)
        {
            extraJump = extraJumpValue;
        }
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
            UpdateAnimatorJumpState();
        }
        else if (Input.GetButtonDown("Jump") && extraJump == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            UpdateAnimatorJumpState();
        }
        UpdateAnimatorJumpState();
    }

    private void UpdateAnimatorJumpState()
    {
        m_animator.SetBool("IsJumping", !isGrounded && rb.velocity.y > 0);
        m_animator.SetBool("IsFalling", !isGrounded && rb.velocity.y < 0);
        if (isGrounded)
        {
            m_animator.SetBool("IsJumping", false);
            m_animator.SetBool("IsFalling", false);
        }
    }
    #endregion
    #region Another Function
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void Dead()
    {
        isDead = true;
    }
    private void start_attack()
    {
        moveSpeed = 0.5f;
    }

    private void end_attack()
    {
        moveSpeed = 6;
    }

    public void knockBack(int force, float isright)
    {
        if (isright < 0)
        {
            rb.AddForce(Vector2.left * force);
        }
        else
        {
            rb.AddForce(Vector2.right * force);
        }
    }
    #endregion
}
