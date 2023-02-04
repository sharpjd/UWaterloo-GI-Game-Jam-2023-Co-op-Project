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
        entities.Add(entity);
        //unfinished
        return true;
    }
}
