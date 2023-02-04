using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : Entity
{
    [SerializeField]
    public int damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity is IHittable)
        {
            IHittable hittable = (IHittable)entity;
            hittable.OnDamage(damage);
            OnDestruction();
        } 
    }

    public void OnDestruction()
    {
        //maybe a hit animation? (could be as simple as a brighten, large, and fade out)
        Destroy(gameObject);
    }

}
