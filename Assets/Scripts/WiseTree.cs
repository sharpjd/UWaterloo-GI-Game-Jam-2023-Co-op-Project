using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiseTree : TowerEntity
{
    [SerializeField]
    float buffRange;

    [SerializeField]
    float buffAmount;

    List<TowerEntity> towersInRange;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        towersInRange = GameHandler.instance.entityTracker.GetTowersInArea(transform.position, buffRange);

        foreach (TowerEntity tower in towersInRange)
        {
            tower.fireRateSecs += buffAmount;
        }

    }

    // Update is called once per frame
    private void OnDestroy()
    {
        foreach (TowerEntity tower in towersInRange)
        {
            tower.fireRateSecs -= buffAmount;
        }
    }
}
