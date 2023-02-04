using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyEntity : Entity, IHittable
{

    [SerializeField]
    int health;

    [SerializeField]
    int essence;

    public int damage;

    public int Hitpoints { get => health; set { health = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileEntity projectileController = collision.gameObject.GetComponent<ProjectileEntity>();
        if (projectileController != null)
        {
            OnDamage(projectileController.damage);
            projectileController.OnDestruction();
        }
    }

    public void OnDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die()
    { 
        GameHandler.instance.GainEssence(essence);
        Destroy(gameObject);
    }

}
