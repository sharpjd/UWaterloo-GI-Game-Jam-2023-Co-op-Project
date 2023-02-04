using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler instance;

    public TMP_Text EssenceText;
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
        EssenceText.SetText(Essence.ToString());
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
            EssenceText.SetText(Essence.ToString());
            return true;
        }

        return false;
    }

    public void GainEssence(int amount)
    {
        Essence += amount;
        EssenceText.SetText(Essence.ToString());
    }
}
