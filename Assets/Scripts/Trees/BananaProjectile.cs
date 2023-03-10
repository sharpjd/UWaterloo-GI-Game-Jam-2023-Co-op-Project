using UnityEngine;

public class BananaProjectile : StandardProjectileEntity
{
    [SerializeField]
    float orbitRadius = 1f;
    [SerializeField]
    float orbitSpeedMultiplier = 1.5f;

    [SerializeField]
    Vector2 origin;

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit(collision.gameObject);
    }

    public override void PostStart()
    {
        base.PostStart();
        origin = transform.position;
    }

    public override void Update()
    {
        Vector2 pos = new Vector2(Mathf.Sin((Time.time - firedTime) * orbitSpeedMultiplier) * orbitRadius, Mathf.Cos((Time.time - firedTime) * orbitSpeedMultiplier) * orbitRadius);

        transform.position = origin + pos;

        if (Time.time - firedTime > lifetimeSeconds)
        {
            OnDestruction();
        }

        PredictCollision();
    }


}
