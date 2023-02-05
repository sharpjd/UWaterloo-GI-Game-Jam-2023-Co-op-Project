using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    public static GameHandler instance;

    public Text EssenceText;
    public Text HealthText;

    public EntityTracker entityTracker;
    public MapPositioner mapPositioner;

    private int Essence = 0;

    private int health = 100;

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
        EssenceText.text = Essence.ToString();
        HealthText.text = health.ToString();
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
            EssenceText.text = Essence.ToString();
            return true;
        }

        return false;
    }

    public void GainEssence(int amount)
    {
        Essence += amount;
        EssenceText.text = Essence.ToString();
    }

    public int GetHealth()
    {
        return health;
    }

    public void LoseHealth(int amount)
    {
        health -= amount;
        HealthText.text = health.ToString() ;
        if (health <= 0)
        {
            Debug.Log("You deaad");
        }
    }
}
