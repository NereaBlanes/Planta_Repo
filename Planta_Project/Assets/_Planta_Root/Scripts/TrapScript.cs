using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 1f;

    bool playerInside = false;
    float nextDamageTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            // Daño instantáneo al entrar
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (!playerInside) return;

        if (Time.time >= nextDamageTime)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    nextDamageTime = Time.time + damageCooldown;
                }
            }
        }
    }
}
