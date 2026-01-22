using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public float invulnerabilityTime = 0.4f;

    int currentHealth;
    bool isDead = false;
    bool isInvulnerable = false;

    private Animator anim;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return;

        currentHealth -= damage;
        Debug.Log("PLAYER VIDA: " + currentHealth);

        if (currentHealth > 0)
        {
            if (anim != null)
            {
                anim.SetTrigger("Hurt");
            }

            // Activar invulnerabilidad
            StartCoroutine(InvulnerabilityCoroutine());
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (!isDead)
            {
                isDead = true;
                Die();
            }
        }
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    void Die()
    {
        Debug.Log("PLAYER MUERTO");

        if (anim != null)
        {
            anim.SetTrigger("Dead");
        }

        PlayerController2D controller = GetComponent<PlayerController2D>();
        if (controller != null)
        {
            controller.Die();
        }

        StartCoroutine(RestartAfterDelay(2.5f));
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}