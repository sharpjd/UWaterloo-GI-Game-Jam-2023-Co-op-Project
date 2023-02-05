using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateProjectile : StandardProjectileEntity
{
    // Start is called before the first frame update

    SpriteRenderer spriteRenderer;
    public override void Start()
    {
        base.Start();
        spriteRenderer= GetComponent<SpriteRenderer>();
        Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        spriteRenderer.color = color;
    }

}
