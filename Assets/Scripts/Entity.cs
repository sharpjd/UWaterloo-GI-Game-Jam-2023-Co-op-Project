using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    List<IEntityExtension> entityExtensions = new();

    [SerializeField]
    bool AddToTargetTrackerAtStart = false;

    public bool AddEntityExtension(IEntityExtension entityExtension)
    {
        entityExtensions.Add(entityExtension);
        return true;
    }

    /*
    Texture2D texture2D;
    Physics2D physics2D;
    */

    private void Awake()
    {
        /*
        texture2D = GetComponent<Texture2D>();
        physics2D = GetComponent<Physics2D>();
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        if (AddToTargetTrackerAtStart)
        {
            GameHandler.instance.entityTracker.AddToTracker(this);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
