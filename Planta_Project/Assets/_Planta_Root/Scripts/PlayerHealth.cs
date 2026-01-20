using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;

    private Animator anim;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("PLAYER VIDA: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("PLAYER MUERTO");

        if (anim != null)
        {
            anim.SetTrigger("Dead");
        }

        PlayerController2D controller = GetComponent<PlayerController2D>();
        if (controller != null) controller.enabled = false;

        // Inicia la corrutina para reiniciar
        StartCoroutine(RestartAfterDelay(2.5f));
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}