using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoTree : TowerEntity
// Start is called before the first frame update
{

    float lastTimeDispense = 0;
    [SerializeField]
    float DispenseRateSecs = 3;

    [SerializeField]
    int essenceGain = 100;

    public override void Update()
    {
        // Update is called once per frame
        
        

        base.Update();
        if (Time.time - lastTimeDispense > DispenseRateSecs)
        {
            GameHandler.instance.GainEssence(essenceGain);
            lastTimeDispense = Time.time;
            Debug.Log("moneyed");
        }
    }
}
