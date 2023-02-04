using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTracker : MonoBehaviour
{

    public List<Entity> entities = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddToTracker(Entity entity)
    {
        if(entities.Contains(entity))
        {
            return false;
        }
        else
        {
            entities.Add(entity);
            return true;
        }
    }

    public bool RemoveFromTracker(Entity entity)
    {
        return entities.Remove(entity);
    }


    public Entity GetClosestEnemy(Vector2 origin, float range)
    {
        float closest = float.MaxValue;
        Entity closestEntity = null;
        foreach (Entity entity in entities)
        {
            float distance = Vector2.Distance(origin, entity.gameObject.transform.position);

            if (distance > range)
                continue;
            if (entity is not EnemyEntity && entity is not IHittable)
                continue;
            
            if (distance < closest)
            {
                closest = distance;
                closestEntity = entity;
            }
        }
        return closestEntity;
    }
}
