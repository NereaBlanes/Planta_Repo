using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform pointA;
    public Transform pointB;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;

    [Header("Detection")]
    public float visionRange = 6f;
    public float stoppingDistance = 3.7f;

    [Header("Attack")]
    public float attackCooldown = 1f;
    public int damage = 1;

    Rigidbody2D rb;

    Vector2 worldPointA;
    Vector2 worldPointB;
    Vector2 currentTarget;

    bool chasingPlayer;
    float nextAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        worldPointA = transform.TransformPoint(pointA.localPosition);
        worldPointB = transform.TransformPoint(pointB.localPosition);

        currentTarget = worldPointB;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(rb.position, player.position);

        chasingPlayer = distanceToPlayer <= visionRange;

        if (chasingPlayer)
            Chase(distanceToPlayer);
        else
            Patrol();

        Flip();
    }

    // ------------------ PATRULLA ------------------
    void Patrol()
    {
        Vector2 dir = (currentTarget - rb.position).normalized;
        rb.linearVelocity = new Vector2(dir.x * patrolSpeed, rb.linearVelocity.y);

        if (Vector2.Distance(rb.position, currentTarget) < 0.2f)
            currentTarget = currentTarget == worldPointA ? worldPointB : worldPointA;
    }

    // ------------------ PERSEGUIR ------------------
    void Chase(float distance)
    {
        if (distance > stoppingDistance)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(dir.x * chaseSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            Attack();
        }
    }

    // ------------------ ATAQUE ------------------
    void Attack()
    {
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + attackCooldown;

        Debug.Log("ENEMIGO ATACA");

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
            health.TakeDamage(damage);
    }

    // ------------------ GIRAR ------------------
    void Flip()
    {
        float targetX = chasingPlayer ? player.position.x : currentTarget.x;

        if (targetX > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // ------------------ DEBUG ------------------
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}