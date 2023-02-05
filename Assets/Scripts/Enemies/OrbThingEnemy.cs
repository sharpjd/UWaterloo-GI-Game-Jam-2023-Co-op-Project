using System;
using System.Collections.Generic;
using UnityEngine;

public class OrbThingEnemy : EnemyEntity
{
    [SerializeField]
    float stunRange = 1f;
    [SerializeField]
    float stunDurationSecs = 2f;

    [SerializeField]
    float stunCooldownSecs = 10f;


    float lastTimeStunned = 0;
    public override void PostUpdate()
    {

        //cooldown for the stun, the rest of the script should just be the stunning stuff
        if (Time.time - lastTimeStunned < stunCooldownSecs) { return; }

        lastTimeStunned = Time.time;

        List<TowerEntity> towerEntities = GameHandler.instance.entityTracker.GetTowersInArea(transform.position, stunRange);

        Debug.Log("Trying to stun " + towerEntities.Count + " enemies");

        foreach (TowerEntity towerEntity in towerEntities)
        {
            //(if it can currently fire)
            bool towerEntityCanFire = towerEntity.CanFire;

            if (towerEntity != null)
            {
                Action before = delegate ()
                {
                    Stun(towerEntity);
                };

                Action after = delegate ()
                {
                    UndoStun(towerEntity, towerEntityCanFire);
                };

                if (towerEntity.GetComponent<TowerEffectApplier>() == null)
                {
                    TowerEffectApplier.instantiateTowerEffectApplier(towerEntity, before, after, stunDurationSecs);
                }

            }
        }

    }

    void Stun(TowerEntity towerEntity)
    {
        towerEntity.CanFire = false;
        towerEntity.SpriteRenderer.color -= new Color(0.3f, 0.3f, 0f, 0f);
        Debug.Log(towerEntity.SpriteRenderer.color);
    }

    void UndoStun(TowerEntity towerEntity, bool previousValue)
    {
        towerEntity.CanFire = previousValue;
        towerEntity.SpriteRenderer.color += new Color(0.3f, 0.3f, 0f, 0f);
    }



}
