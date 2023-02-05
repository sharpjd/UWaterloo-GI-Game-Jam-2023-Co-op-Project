using Assets;
using UnityEngine;

public class StandardProjectileEntity : Entity
{
    [SerializeField]
    public int damage = 1;

    [SerializeField]
    public float velocityPerSecond = 1f;

    [SerializeField]
    protected float lifetimeSeconds = 10f;

    protected float firedTime;

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit(collision.gameObject);
    }

    public virtual void OnHit(GameObject gameObject_)
    {
        Entity entity = gameObject_.GetComponent<Entity>();
        if (entity is IHittable)
        {
            IHittable hittable = (IHittable)entity;
            hittable.OnDamage(damage);
            OnDestruction();
        }
    }

    public virtual void OnDestruction()
    {
        //maybe a hit animation? (could be as simple as a brighten, large, and fade out)
        Destroy(gameObject);
    }

    public override void Start()
    {
        base.Start();
        firedTime = Time.time;
        PostStart();
    }

    public virtual void PostStart()
    {

    }

    public override void Update()
    {
        base.Update();
        transform.position += (Vector3)((Vector2)transform.right * velocityPerSecond * Time.deltaTime);

        if (Time.time - firedTime > lifetimeSeconds)
        {
            OnDestruction();
        }

        PredictCollision();

        postUpdate();
    }

    protected void PredictCollision()
    {
        int layermask = 1 << 2;
        layermask = ~layermask;

        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, CurrentVelocityPerSecondVec.normalized, CurrentVelocityPerSecondVec.magnitude * Time.fixedDeltaTime, layermask);
        //Debug.Log(hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            OnHit(hits[i].collider.gameObject);
        }
    }

    public virtual void postUpdate()
    {

    }

}
