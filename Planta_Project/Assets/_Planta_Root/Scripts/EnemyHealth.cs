using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3; // Vida máxima
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Detectar hitbox externa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si la hitbox tiene la etiqueta "Weapon"
        if (collision.CompareTag("Weapon"))
        {
            TakeDamage(1); // Restar 1 de vida (puedes cambiar el valor)
        }
    }

    // Aplica el daño
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
