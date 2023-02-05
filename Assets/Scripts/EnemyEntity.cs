using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyEntity : Entity, IHittable
{

    [SerializeField]
    int health;

    [SerializeField]
    int essence;

    [SerializeField]
    float progressPerSecond = 0.05f;

    [SerializeField]
    float currentProgress = 0;

    public int damage;



    public int Health { get => health; set { health = value; } }
    public float ProgressPerSecond { get => progressPerSecond; set => progressPerSecond = value; }

    public override void Start()
    {
        base.Start();
        transform.position = GameHandler.instance.mapPositioner.GetPositionOnMapByProgress(0);
    }

    public virtual void OnDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("I am dead");
            Die();
        }
    }

    public override void Update()
    {
        base.Update();
        currentProgress += progressPerSecond * Time.deltaTime;
        transform.position = GameHandler.instance.mapPositioner.GetPositionOnMapByProgress(currentProgress);

        PostUpdate();
    }

    public virtual void PostUpdate() {

    }

    public virtual void Die()
    { 
        GameHandler.instance.GainEssence(essence);
        GameHandler.instance.entityTracker.RemoveFromTracker(this);
        Destroy(gameObject);
    }

    //just in case
    private void OnDestroy()
    {
        GameHandler.instance.entityTracker.RemoveFromTracker(this);
    }

}
