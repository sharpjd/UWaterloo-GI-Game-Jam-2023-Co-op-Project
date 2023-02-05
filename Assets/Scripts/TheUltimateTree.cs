using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TheUltimateTree : TowerEntity
{
    public override void FireProjectile(Entity target)
    {
        base.FireProjectile(target);
        for (int i = -15; i <= 15; i+= 5)
        {
            FireAtAngle(target, i);
        }
    }

    public void FireAtAngle(Entity target, float angle)
    {
        base.FireProjectile(target);

        GameObject projectile = Instantiate(projectileToInstantiate);
        projectile.transform.position = transform.position;
        Vector2 predictedTargetDirection = PositioningUtils.PredictShotToTarget(transform.position, target.transform, projectileVelocity, target.CurrentVelocityPerSecondVec);

        if (shotPrediction)
            projectile.transform.rotation = PositioningUtils.LookFromToAt(transform.position, predictedTargetDirection);
        else
            projectile.transform.rotation = PositioningUtils.LookFromToAt(transform.position, target.transform.position);

        Vector3 rotationToAdd = new Vector3(0, 0, angle);
        projectile.transform.Rotate(rotationToAdd);

        projectile.SetActive(true);
    }
}
 