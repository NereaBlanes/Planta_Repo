using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement & Jump Configuration")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isFacingRight; //Define la orientación del personaje
    [SerializeField] Transform groundCheck; //Posición del detector del suelo
    [SerializeField] float groundCheckRadius; //Define el radio del círculo detector de suelo
    [SerializeField] LayerMask groundLayer; //Define la capa que puede tocar el detector de suelo
    [SerializeField] GameObject attackHitbox;

    //Variables de referencia general
    Rigidbody2D playerRb; //Almacén del rigidbody del player
    Animator anim; //Almacén del controlador de animaciones del player
    PlayerInput input; //Almacén del controlador de inputs del player
    Vector2 moveInput; //Almacén del valor de los botones de movimiento
    bool canAttack; //bool de seguridad que define si  se puede atacar o no

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>(); //Autoreferenciar un componente propio
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        canAttack = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Lógica de detección del suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Lógica de las animaciones
        AnimationManagement();
        //Lógica del flip del personaje
        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //Mover el motor de aceleración del rigidbody
        playerRb.linearVelocity = new Vector2(moveInput.x * speed, playerRb.linearVelocity.y);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale; //Almacén temporal de la escala del objeto
        currentScale.x *= -1; //Invertir el valor en X
        transform.localScale = currentScale; //Le devolvemos la escala al objeto con el valor en x inverso
        isFacingRight = !isFacingRight; //Decirle al bool que cambie al valor contrario
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        AudioManager.Instance.PlaySFX(3);
    }
    IEnumerator Attack()
    {
        canAttack = false; //Quitar la posibilidad de atacar
        float actualSpeed = speed; //Guardamos la velocidad actual para devolerla luego
        speed = 0; //Con velocidad 0 el personaje se queda quieto
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        speed = actualSpeed;
        canAttack = true;
        //Devolvemos velocidad y capacidad de ataque al jugador, se acaba la corrutina
        yield return null;
    }

    void AnimationManagement()
    {
        //Acción para gestionar los cambios de animación
        anim.SetBool("Jump", !isGrounded);
        if (moveInput.x != 0) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);
    }





    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) Jump();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && canAttack) StartCoroutine(Attack());
    }

    #endregion
}
