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

    public int Health { get => health; set { health = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
