using System;
using UnityEngine;


/// <summary>
/// Note: it is easier to instantiate this with <see cref="instantiateEnemyEntityEffectApplier(EnemyEntity, System.Action, System.Action, float)"/>
/// Keep in mind that stacking instances of this will also cause misbehavior.
/// </summary>
class EnemyEntityEffectApplier : MonoBehaviour
{
    [SerializeField]
    float startTime;

    Action actionBefore;
    Action actionAfter;

    [SerializeField]
    float durationSeconds;

    public EnemyEntity EnemyEntityToApplyEffectTo;

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

    public static EnemyEntityEffectApplier instantiateEnemyEntityEffectApplier (EnemyEntity enemyEntity, Action actionBefore, Action actionAfter, float durationSeconds)
    {
        EnemyEntityEffectApplier enemyEffectApplier = enemyEntity.gameObject.AddComponent<EnemyEntityEffectApplier>();
        enemyEffectApplier.startTime = Time.time;

        enemyEffectApplier.actionBefore = actionBefore;
        enemyEffectApplier.actionAfter = actionAfter;

        enemyEffectApplier.durationSeconds = durationSeconds;

        enemyEffectApplier.EnemyEntityToApplyEffectTo = enemyEntity;

        enemyEffectApplier.enabled = true;

        return enemyEffectApplier;
    }

}

public enum EffectType
{
    AppleTree,
    MapleTree
}