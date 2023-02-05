using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherTree : TowerEntity
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

        foreach (MotherTree tower in towersInRange)
        {
            if (tower.gameObject.GetComponent<FatherTree>() != null)
            {
                fireRateSecs /= buffAmount;
            }

        }
    }
}