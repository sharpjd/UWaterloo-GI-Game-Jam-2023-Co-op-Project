using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathTester : MonoBehaviour
{
    float progressPerSecond = 0.1f;
    float initialProgress = 0;
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
        transform.position = GameHandler.instance.mapPositioner.GetPositionOnMapByProgress(currentProgress);
    }
}
