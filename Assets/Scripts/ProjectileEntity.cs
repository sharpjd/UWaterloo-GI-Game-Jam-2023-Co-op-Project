using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : Entity
{
    [SerializeField]
    public int damage = 1;

    [SerializeField]
    public float speedPerSecond = 1f;

    [SerializeField]
    float lifetimeSeconds = 10f;

    float firedTime;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity is IHittable)
        {
            IHittable hittable = (IHittable)entity;
            hittable.OnDamage(damage);
            OnDestruction();
        } 
    }

    public void OnDestruction()
    {
        //maybe a hit animation? (could be as simple as a brighten, large, and fade out)
        Destroy(gameObject);
    }

    private void Start()
    {
        firedTime = Time.time;
    }

    public void Update()
    {
        transform.position += (Vector3)((Vector2)transform.right * speedPerSecond * Time.deltaTime);

        if(Time.time - firedTime > lifetimeSeconds)
        {
            OnDestruction();
        }
    }

}
