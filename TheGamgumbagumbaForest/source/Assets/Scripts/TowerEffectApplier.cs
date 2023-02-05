using System;
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

        startEffect();

        startTime = Time.time;
    }

    public void startEffect()
    {
        actionBefore.Invoke();
    }
    public void endEffect()
    {
        actionAfter.Invoke();
    }

    private void Update()
    {
        if (Time.time - startTime > durationSeconds)
        {
            endEffect();
            Destroy(this);
        }
    }

    public static TowerEffectApplier instantiateTowerEffectApplier(TowerEntity towerEntity, Action actionBefore, Action actionAfter, float durationSeconds)
    {
        TowerEffectApplier towerEffectApplier = towerEntity.gameObject.AddComponent<TowerEffectApplier>();
        towerEffectApplier.startTime = Time.time;

        towerEffectApplier.actionBefore = actionBefore;
        towerEffectApplier.actionAfter = actionAfter;

        towerEffectApplier.durationSeconds = durationSeconds;

        towerEffectApplier.TowerEntityToApplyAffectTo = towerEntity;

        towerEffectApplier.enabled = true;


        return towerEffectApplier;
    }
}
