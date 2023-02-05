using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEffectApplier : MonoBehaviour
{
    [SerializeField]
    float startTime;

    Action actionBefore;
    Action actionAfter;

    [SerializeField]
    float durationSeconds;

    public TowerEntity TowerEntityToApplyAffectTo;

    private void Awake()
    {
        enabled = false;
    }

    private void Start()
    {

        actionBefore.Invoke();

        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime > durationSeconds)
        {
            actionAfter.Invoke();
            Destroy(this);
        }
    }

   public static TowerEffectApplier instantiateTowerEffectApplier(TowerEntity towerEntity, Action actionBefore, Action actionAfter, float durationSeconds)
    {
        TowerEffectApplier towerEffectApplier = towerEntity.gameObject.AddComponent<TowerEffectApplier>();
        towerEffectApplier.startTime = Time.time;

        towerEffectApplier.actionBefore = actionBefore;
        towerEffectApplier.actionAfter  = actionAfter;

        towerEffectApplier.durationSeconds = durationSeconds;

        towerEffectApplier.TowerEntityToApplyAffectTo = towerEntity;

        towerEffectApplier.enabled = true;


        return towerEffectApplier;
    }
}
