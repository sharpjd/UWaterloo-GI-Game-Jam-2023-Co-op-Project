using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Entity : MonoBehaviour
{

    List<IEntityExtension> entityExtensions = new();

    [SerializeField]
    bool AddToTargetTrackerAtStart = false;

    [SerializeField]
    float currentVelocityPerSecond;
    public float CurrentVelocityPerSecond { get => currentVelocityPerSecond; }

    [SerializeField]
    Vector2 currentVelocityPerSecondVec;
    public Vector2 CurrentVelocityPerSecondVec { get => currentVelocityPerSecondVec; }

    public bool AddEntityExtension(IEntityExtension entityExtension)
    {
        entityExtensions.Add(entityExtension);
        return true;
    }

    /*
    Texture2D texture2D;
    Physics2D physics2D;
    */

    public virtual void Awake()
    {
        /*
        texture2D = GetComponent<Texture2D>();
        physics2D = GetComponent<Physics2D>();
        */
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        previousPos = transform.position;

        if (AddToTargetTrackerAtStart)
        {
            GameHandler.instance.entityTracker.AddToTracker(this);  
        }
    }

    Vector2 previousPos;
    // Update is called once per frame
    public virtual void Update()
    {
        currentVelocityPerSecond = ((Vector2)transform.position - previousPos).magnitude/Time.deltaTime;
        currentVelocityPerSecondVec = ((Vector2)transform.position - previousPos)/Time.deltaTime;
        previousPos = transform.position;
    }
}
