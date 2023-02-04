using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : Entity
{
    [SerializeField]
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("enemy"))
        {
            // Do stuff
        }
    }

    public void OnDestruction()
    {
        //maybe a hit animation? (could be as simple as a brighten, large, and fade out)
        Destroy(gameObject);
    }

}
