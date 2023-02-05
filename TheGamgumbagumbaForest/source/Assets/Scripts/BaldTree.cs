using System.Collections.Generic;
using UnityEngine;

public class BaldTree : TowerEntity
{
    [SerializeField]
    float buffRange;

    //[SerializeField]
    //float buffAmount;

    List<TowerEntity> towersInRange;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        towersInRange = GameHandler.instance.entityTracker.GetTowersInArea(transform.position, buffRange);

        foreach (TowerEntity tower in towersInRange)
        {
            //tower.fireRateSecs /= buffAmount;
            //tick's the cannot be stunned box
            tower.isImmunetoStun = true;
        }

    }

}
