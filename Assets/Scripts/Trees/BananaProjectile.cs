using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BananaProjectile : Entity
{
    [SerializeField]
    float orbitRadius = 1f;
    [SerializeField]
    float orbitCompletionTimeSeconds = 1.5f;
    [SerializeField]
    public int damage = 1;
    [SerializeField]
    float lifetimeSeconds = 10f;

    float firedTime;

    [SerializeField]
    Vector2 origin;

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
        origin = transform.position;
        firedTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        Vector2 pos = new Vector2(Mathf.Sin(Time.time - firedTime) * orbitRadius, Mathf.Sin(Time.time - firedTime) * orbitRadius);

        transform.position = origin + pos;


        if (Time.time - firedTime > lifetimeSeconds)
        {
            OnDestruction();
        }
    }


}
