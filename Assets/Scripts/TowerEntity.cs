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

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = towerSprite;
    }

    // Update is called once per frame
    float lastTimeFired = 0;
    void Update()
    {
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

        Vector2 yx = (Vector2)target.transform.position - (Vector2)transform.position;
        projectile.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(yx.y, yx.x) * Mathf.Rad2Deg, Vector3.forward);

        projectile.SetActive(true);
    }
}
