using Assets;
using System;
using UnityEngine;

public class SlowProjectile : StandardProjectileEntity
{

    [SerializeField]
    float knockbackProgressFactor = -0.5f;
    [SerializeField]
    float knockbackDurationSeconds = 5f;
    [SerializeField]
    EffectType effectType = EffectType.AppleTree;

    public override void OnHit(GameObject gameObject_)
    {
        Entity entity = gameObject_.gameObject.GetComponent<Entity>();
        if (entity is IHittable)
        {
            IHittable hittable = (IHittable)entity;
            hittable.OnDamage(damage);

            if (entity is EnemyEntity)
            {

                EnemyEntity enemyEntity = (EnemyEntity)entity;
                float previousEnemyEntitySpeed = enemyEntity.ProgressPerSecond;
                //prevent stacking
                if (entity.GetComponent<EnemyEntityEffectApplier>() == null)
                {

                    Action before = delegate ()
                    {
                        Slow(enemyEntity);
                    };
                    Action after = delegate ()
                    {
                        Unslow(enemyEntity, previousEnemyEntitySpeed);
                    };

                    EnemyEntityEffectApplier.instantiateEnemyEntityEffectApplier(
                    (EnemyEntity)entity, before, after, knockbackDurationSeconds
                    );
                }
            }

            OnDestruction();
        }
    }

    void Slow(EnemyEntity enemyEntity)
    {
        enemyEntity.ProgressPerSecond *= knockbackProgressFactor;
    }
    void Unslow(EnemyEntity enemyEntity, float previousProgressPerSecond)
    {
        enemyEntity.ProgressPerSecond = previousProgressPerSecond;
    }
}
