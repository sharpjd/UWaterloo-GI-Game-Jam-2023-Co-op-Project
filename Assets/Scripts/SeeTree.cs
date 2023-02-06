using System.Collections.Generic;
using UnityEngine;

public class SeeTree : TowerEntity
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
            tower.Range *= buffAmount;
        }

    }
}
