using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement & Jump Configuration")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isFacingRight;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject attackHitbox;

    Rigidbody2D playerRb;
    Animator anim;
    PlayerInput input;
    Vector2 moveInput;
    bool canAttack;
    bool isDead;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        canAttack = true;
        isDead = false;
    }

    void Start()
    {
        isFacingRight = true;
    }

    void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        AnimationManagement();

        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            playerRb.linearVelocity = Vector2.zero;
            return;
        }

        Movement();
    }

    void Movement()
    {
        playerRb.linearVelocity = new Vector2(moveInput.x * speed, playerRb.linearVelocity.y);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }

    void Jump()
    {
        if (isDead) return;

        playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        AudioManager.Instance.PlaySFX(3);
    }

    IEnumerator Attack()
    {
        if (isDead) yield break;

        canAttack = false;
        float actualSpeed = speed;
        speed = 0;

        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        speed = actualSpeed;
        canAttack = true;
    }

    void AnimationManagement()
    {
        anim.SetBool("Jump", !isGrounded);
        anim.SetBool("Run", moveInput.x != 0);
    }

    public void Die()
    {
        isDead = true;

        moveInput = Vector2.zero;
        playerRb.linearVelocity = Vector2.zero;

        anim.SetBool("Run", false);
        anim.SetBool("Jump", false);
        anim.SetTrigger("Death");
    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isDead) return;
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isDead) return;
        if (context.performed && isGrounded) Jump();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isDead) return;
        if (context.performed && isGrounded && canAttack)
            StartCoroutine(Attack());
    }

    #endregion
}
