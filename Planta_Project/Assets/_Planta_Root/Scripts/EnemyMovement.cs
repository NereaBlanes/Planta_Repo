using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Movimiento")]
    public float speed = 2f;
    public float visionRange = 6f;
    public float stoppingDistance = 1.8f;

    [Header("Ataque")]
    public int damage = 1;
    public float attackCooldown = 1f;

    Rigidbody2D rb;
    bool facingRight = true;
    float lastAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player NO asignado");
            return;
        }

        float distance = Vector2.Distance(rb.position, player.position);
        Debug.Log("DISTANCIA AL JUGADOR: " + distance);

        if (distance <= visionRange)
        {
            Move(distance);
            Flip();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void Move(float distance)
    {
        if (distance > stoppingDistance)
        {
            float dir = Mathf.Sign(player.position.x - rb.position.x);
            rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
            Debug.Log("MOVI�NDOME HACIA EL JUGADOR");
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            Debug.Log("EN RANGO DE ATAQUE");
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
        {
            Debug.Log("COOLDOWN...");
            return;
        }

        Debug.Log("INTENTO ATACAR");

        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            Debug.Log("HAGO DA�O AL JUGADOR");
            ph.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("PlayerHealth NO encontrado en el Player");
        }

        lastAttackTime = Time.time;
    }

    void Flip()
    {
        if (player.position.x > transform.position.x && !facingRight)
            DoFlip();
        else if (player.position.x < transform.position.x && facingRight)
            DoFlip();
    }

    void DoFlip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}