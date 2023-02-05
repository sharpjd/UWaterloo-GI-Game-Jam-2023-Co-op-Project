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

    public float range = 3f;
    public float fireRateSecs = 0.5f;

    [SerializeField]
    bool debugOutput = false;
    [SerializeField]
    bool shotPrediction = true;

    //obtained from projectileToInstantiate
    float projectileVelocity;

    [SerializeField]
    bool canFire = true;
    public bool CanFire { get => canFire; set => canFire = value; }

    public override void Start()
    {
        base.Start();
        projectileVelocity = projectileToInstantiate.GetComponent<StandardProjectileEntity>().velocityPerSecond;
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

    public virtual void OnProjectileFire()
    {

        if (!canFire) return;

        Entity closestTarget = 
            GameHandler.instance.entityTracker.GetClosestEnemy(transform.position, range);

        if (closestTarget != null)
        {
            FireProjectile(closestTarget);
        }
    }

    public virtual void FireProjectile(Entity target)
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
