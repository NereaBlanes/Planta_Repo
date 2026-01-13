using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthFill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthFill.fillAmount = GameManager.Instance.playerHeatlh / GameManager.Instance.maxHealth;
    }
}
