using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.PlayerSettings;

//TODO:
public class TowerEntity : Entity
{
    // Start is called before the first frame update

    public Sprite towerSprite;

    [SerializeField]
    GameObject projectileToInstantiate;
    [SerializeField]
    float range = 3f;
    [SerializeField]
    float fireRateSecs = 0.5f;
    [SerializeField]
    bool debugOutput = false;
    [SerializeField]
    bool shotPrediction = true;

    //obtained from projectileToInstantiate
    float projectileVelocity;

    public override void Start()
    {
        base.Start();
        projectileVelocity = projectileToInstantiate.GetComponent<ProjectileEntity>().velocityPerSecond;
        gameObject.GetComponent<SpriteRenderer>().sprite = towerSprite;
    }

    // Update is called once per frame
    float lastTimeFired = 0;
    public override void Update()
    {
        base.Update();
        if(Time.time - lastTimeFired > fireRateSecs) {
            OnProjectileFire();
            lastTimeFired= Time.time;
            Debug.Log("fired");
        }
    }

    void OnProjectileFire()
    {

        Entity closestTarget = 
            GameHandler.instance.entityTracker.GetClosestEnemy(transform.position, range);

        FireProjectile(closestTarget);
    }

    void FireProjectile(Entity target)
    {
        if(debugOutput)
            Debug.DrawLine(transform.position, target.transform.position, Color.red, 3f);

        GameObject projectile = Instantiate(projectileToInstantiate);
        projectile.transform.position = transform.position;

        Vector2 predictedTargetDirection = PositioningUtils.PredictShotToTarget(transform.position, target.transform, projectileVelocity, target.CurrentVelocityPerSecondVec);
        if (shotPrediction)
            projectile.transform.rotation = PositioningUtils.LookFromToAt(transform.position, predictedTargetDirection);
        else
            projectile.transform.rotation = PositioningUtils.LookFromToAt(transform.position, target.transform.position);

        projectile.SetActive(true);
    }
}
