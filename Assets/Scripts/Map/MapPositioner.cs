using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPositioner : MonoBehaviour
{

    float sumOfDistance;

    List<MapPathPoint> MapPathPoints= new List<MapPathPoint>();

    public MapPathPoint firstMapPathPoint;

    private void Awake()
    {

        //find the first point in the sequence and make an array sorted in order of the points
        //then assign it to MapPathPoints
        //the sort is required because the progress algorithm depends on it
        int points = 0;
        MapPathPoint firstPoint;

        MapPathPoint[] mapPathPointsToSort = FindObjectsOfType<MapPathPoint>();
        points = mapPathPointsToSort.Length;

        MapPathPoint[] sortedMapPathPoints = new MapPathPoint[points];

        foreach (MapPathPoint mapPathPoint in mapPathPointsToSort)
        {
            sortedMapPathPoints[mapPathPoint.NumberInPointSequence] = mapPathPoint;

            if(mapPathPoint.NumberInPointSequence == 0)
            {
                firstPoint = mapPathPoint;
            }
        }
        MapPathPoints = new List<MapPathPoint>(sortedMapPathPoints);
        
        /*
        foreach(MapPathPoint mapPathPoint in MapPathPoints)
        {
            Debug.Log(mapPathPoint.NumberInPointSequence + ", " + mapPathPoint.gameObject.transform.position, mapPathPoint);
        }*/

        //find the sum of the distance between all points and the ratio of distance/percentage
        for (int i = 1; i < MapPathPoints.Count; i++)
        {
            sumOfDistance += Vector2.Distance(
                    MapPathPoints[i - 1].gameObject.transform.position,
                    MapPathPoints[i].gameObject.transform.position
                    );
        }

    }

    /// <summary>
    /// </summary>
    /// <param name="progress">The percentage progress; 0.0 representing 0% and 1.0 representing 100%</param>
    /// <returns></returns>
    public Vector2 GetPositionOnMapByProgress(float progress)
    {
        //total distance along the line composed of all the linear lines
        float distance = sumOfDistance * progress;

        float progressRemaining = progress;

        //represents a linear line between these two points
        MapPathPoint point1 = null;
        MapPathPoint point2 = null;

        for (int i = 0; i < MapPathPoints.Count-1; i++)
        {

            if (MapPathPoints[i] == null)
            {
                Debug.LogError("Missing map path point " + i);
            }

            //this is the distance between the current point and the next, starting from point 0
            float deltaDistance = distance - Vector2.Distance(
                    MapPathPoints[i].gameObject.transform.position,
                    MapPathPoints[i+1].gameObject.transform.position
                    );
            //if the remaining distance minus the distance between
            //this point and the next point is less than zero,
            //then the progress is between the following two points
            //Debug.Log(deltaDistance);
            if(deltaDistance < 0)
            {
                point1 = MapPathPoints[i];
                point2 = MapPathPoints[i+1];
                break;
            } else
            {
                //else, just keep the loop going
                distance -= deltaDistance;
                progressRemaining -= deltaDistance / sumOfDistance;
            }
        }

        //A nullreference will be thrown here if the above loop is not working correctly
        float distanceBetweenP1P2 = Vector2.Distance(point1.transform.position, point2.transform.position);

        Debug.DrawLine(point1.transform.position, point2.transform.position);
        Debug.Log("1: " + point1.transform.position, point1);
        Debug.Log("2: " + point2.transform.position, point2);

        Vector2 dir = point2.transform.position - point1.transform.position;
        dir.Normalize();

        dir *= (progressRemaining * sumOfDistance);

        //Debug.Log("Dir: " + dir);

        return (Vector2)point1.transform.position + dir;
    }


}