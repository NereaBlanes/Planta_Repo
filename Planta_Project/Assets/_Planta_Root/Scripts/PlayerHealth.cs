using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    bool isDead = false;

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
            currentHealth = 0;

            if (!isDead)
            {
                isDead = true;
                Die();
            }
            
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