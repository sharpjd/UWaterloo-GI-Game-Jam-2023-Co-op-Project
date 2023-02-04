using Assets;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleProjectile : ProjectileEntity
{

    [SerializeField]
    float knockbackProgressFactor = -0.5f;
    [SerializeField]
    float knockbackDurationSeconds = 5f;


    public override void OnHit(Collider2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity is IHittable)
        {
            IHittable hittable = (IHittable)entity;
            hittable.OnDamage(damage);

            if(entity is EnemyEntity)
            {
                EntityProgressChangerUnchanger.instantiateEntityProgressChangerUnchanger(
                    (EnemyEntity)entity, knockbackProgressFactor, knockbackDurationSeconds
                );
            }

            OnDestruction();
        }
    }
}
