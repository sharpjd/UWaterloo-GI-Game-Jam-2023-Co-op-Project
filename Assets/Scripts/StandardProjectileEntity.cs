using Assets;
using System.Collections;
using System.Collections.Generic;
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
        OnHit(collision);
    }

    public virtual void OnHit(Collider2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
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

        if(Time.time - firedTime > lifetimeSeconds)
        {
            OnDestruction();
        }

        postUpdate();
    }

    public virtual void postUpdate()
    {

    }

}
