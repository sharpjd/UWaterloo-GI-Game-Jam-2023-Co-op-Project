using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    int health;

    [SerializeField]
    int essence;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileController projectileController = collision.gameObject.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            health -= projectileController.damage;
            Destroy(projectileController.gameObject);
            if (health <= 0) Die();
        }
    }

    void Die()
    {
        GameHandler.instance.GainEssence(essence);
        Destroy(gameObject);
    }

}
