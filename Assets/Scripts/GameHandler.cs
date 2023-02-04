using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler instance;

    public EntityTracker entityTracker;
    public MapPositioner mapPositioner;

    private int Essence = 0;

    private void Awake()
    {
        instance = this;

        if (entityTracker == null)
            Debug.LogError("Missing EntityTracker instance");
        if (mapPositioner == null)
            Debug.LogError("Missing MapPositioner instance");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetEssence()
    {
        return Essence;
    }

    public bool PayEssence(int amount)
    {
        if (Essence >= amount)
        {
            Essence -= amount;
            return true;
        }

        return false;
    }
}
