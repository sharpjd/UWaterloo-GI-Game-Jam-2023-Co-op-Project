using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathTester : MonoBehaviour
{
    [SerializeField]
    float progressPerSecond = 0.1f;
    [SerializeField]
    float initialProgress = 0;
    [SerializeField]
    float currentProgress;

    // Start is called before the first frame update
    void Start()
    {
        currentProgress = initialProgress;
        transform.position = GameHandler.instance.mapPositioner.firstMapPathPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentProgress += progressPerSecond * Time.deltaTime;
        /*
        Debug.Log("1" + GameHandler.instance);
        Debug.Log("2" + GameHandler.instance.mapPositioner);
        Debug.Log("3" + GameHandler.instance.mapPositioner.GetPositionOnMapByProgress(currentProgress));
        */
        transform.position = GameHandler.instance.mapPositioner.GetPositionOnMapByProgress(currentProgress);
    }
}
