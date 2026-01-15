using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] LayerMask enemyLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
