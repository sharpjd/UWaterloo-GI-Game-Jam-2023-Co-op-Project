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

    public override void Start()
    {
        base.Start();
        GameHandler.instance.entityTracker.AddToTracker(this);
    }

    public virtual void OnDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("I am dead");
            Die();
        }
    }

    public virtual void Die()
    { 
        GameHandler.instance.GainEssence(essence);
        GameHandler.instance.entityTracker.RemoveFromTracker(this);
        Destroy(gameObject);
    }

}
