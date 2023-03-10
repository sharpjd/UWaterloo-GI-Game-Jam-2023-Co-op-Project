
using UnityEngine;

//TODO:
public class TowerEntity : Entity
{
    // Start is called before the first frame update

    public GameObject CantPlaceHereHitbox;
    [SerializeField]
    CantPlaceHere cantPlaceHereScript;

    public Sprite towerSprite;

    public GameObject RangeIndicator;
    Collider2D mouseoverCollider;

    [SerializeField]
    protected GameObject projectileToInstantiate;

    public float range = 3f;
    public float fireRateSecs = 0.5f;

    [SerializeField]
    bool debugOutput = false;
    [SerializeField]
    protected bool shotPrediction = true;

    public bool isImmunetoStun = false;

    //obtained from projectileToInstantiate
    protected float projectileVelocity;

    [SerializeField]
    bool canFire = true;
    public bool CanFire { get => canFire; set => canFire = value; }
    public CantPlaceHere CantPlaceHereHitbox1 { get => cantPlaceHereScript; }

    public override void Awake()
    {

        mouseoverCollider = gameObject.AddComponent<BoxCollider2D>();

        CantPlaceHereHitbox = gameObject.GetComponentInChildren<CantPlaceHere>().gameObject;

        RangeIndicator = transform.Find("RangeIndicator").gameObject;

        base.Awake();
        cantPlaceHereScript = CantPlaceHereHitbox?.gameObject.GetComponent<CantPlaceHere>();

        if (cantPlaceHereScript == null)
        {
            Debug.LogError("Missing CantPlaceHere reference");
        }

    }

    public override void Start()
    {
        base.Start();
        RangeIndicator.transform.localScale = new Vector3(range,range);

        projectileVelocity = projectileToInstantiate.GetComponent<StandardProjectileEntity>().velocityPerSecond;
        gameObject.GetComponent<SpriteRenderer>().sprite = towerSprite;
    }

    // Update is called once per frame
    float lastTimeFired = 0;

    public override void Update()
    {
        base.Update();
        if (Time.time - lastTimeFired > fireRateSecs)
        {
            OnProjectileFire();
            lastTimeFired = Time.time;
            //Debug.Log("fired");
        }

    }

    private void OnMouseOver()
    {
        RangeIndicator.SetActive(true);
    }

    private void OnMouseExit()
    {
        RangeIndicator.SetActive(false);
    }


    public virtual void OnProjectileFire()
    {

        if (!canFire && !isImmunetoStun) return;

        Entity closestTarget =
            GameHandler.instance.entityTracker.GetClosestEnemy(transform.position, range);

        if (closestTarget != null)
        {
            FireProjectile(closestTarget);
        }
    }

    public virtual void FireProjectile(Entity target)
    {
        if (debugOutput)
            Debug.DrawLine(transform.position, target.transform.position, Color.red, 3f);

        GameObject projectile = Instantiate(projectileToInstantiate);

        StandardProjectileEntity standardProjectileEntity = projectile.GetComponent<StandardProjectileEntity>();
        standardProjectileEntity.range = range;

        projectile.transform.position = transform.position;

        Vector2 predictedTargetDirection = PositioningUtils.PredictShotToTarget(transform.position, target.transform, projectileVelocity, target.CurrentVelocityPerSecondVec);

        if (shotPrediction)
            projectile.transform.rotation = PositioningUtils.LookFromToAt(transform.position, predictedTargetDirection);
        else
            projectile.transform.rotation = PositioningUtils.LookFromToAt(transform.position, target.transform.position);

        projectile.SetActive(true);
    }
}
