using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoints & Movement Configuration")]
    [SerializeField] float speed;
    [SerializeField] Transform[] points;
    [SerializeField] int startingPoint;

    int i;
    Rigidbody2D rb;
    Vector2 lastPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        i = startingPoint;
        rb.position = points[i].position;
        lastPosition = rb.position;
    }

    void FixedUpdate()
    {
        Vector2 target = points[i].position;
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, target) < 0.02f)
        {
            i++;
            if (i >= points.Length)
                i = 0;
        }

        lastPosition = rb.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        // Movimiento real de la plataforma
        Vector2 platformVelocity = (rb.position - lastPosition) / Time.fixedDeltaTime;

        Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.position += platformVelocity * Time.fixedDeltaTime;
        }
    }
}