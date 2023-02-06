using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    float speed;

    void Start()
    {
        button.onClick.AddListener(onButtonClick);
    }

    private void Update()
    {
        if(Time.timeScale != speed)
        {
            gameObject.GetComponent<Image>().color = new Color(0.7f,0.7f,0.7f);
        }
        if (Time.timeScale == speed)
        {
            gameObject.GetComponent<Image>().color = new Color(1,1,1);
        }

    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    void onButtonClick()
    {
        Time.timeScale = speed;
    }
}
