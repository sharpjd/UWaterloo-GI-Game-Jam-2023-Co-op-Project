using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        OnHit(collision);
    }

    public override void Update()
    {
        Vector2 pos = new Vector2(Mathf.Sin((Time.time - firedTime)*orbitSpeedMultiplier) * orbitRadius, Mathf.Sin((Time.time - firedTime)) * orbitSpeedMultiplier * orbitRadius);

        transform.position = origin + pos;

        if (Time.time - firedTime > lifetimeSeconds)
        {
            OnDestruction();
        }
    }


}
