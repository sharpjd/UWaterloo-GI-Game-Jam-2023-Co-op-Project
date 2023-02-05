using UnityEngine;

public class UltimateProjectile : StandardProjectileEntity
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        SpriteRenderer.color = color;
    }

}
