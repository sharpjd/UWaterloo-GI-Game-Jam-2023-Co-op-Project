using Assets;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlowProjectile : StandardProjectileEntity
{

    [SerializeField]
    float knockbackProgressFactor = -0.5f;
    [SerializeField]
    float knockbackDurationSeconds = 5f;
    [SerializeField]
    SlowType slowType = SlowType.AppleTree;

    public override void OnHit(GameObject gameObject_)
    {
        Entity entity = gameObject_.gameObject.GetComponent<Entity>();
        if (entity is IHittable)
        {
            IHittable hittable = (IHittable)entity;
            hittable.OnDamage(damage);

            if (entity is EnemyEntity)
            {
                //prevent stacking
                if (entity.GetComponent<EntityProgressChangerUnchanger>() == null)
                { 
                    EntityProgressChangerUnchanger.instantiateEntityProgressChangerUnchanger(
                    (EnemyEntity)entity, knockbackProgressFactor, knockbackDurationSeconds, slowType
                    );
                }
            }

            OnDestruction();
        }
    }
}
