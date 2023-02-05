using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTree : TowerEntity
{
    // Start is called before the first frame update
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
            if (tower is FatherTree)
            {
                fireRateSecs /= buffAmount;
            }

        }

    }
}