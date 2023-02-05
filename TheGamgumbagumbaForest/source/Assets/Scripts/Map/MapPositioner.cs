using System.Collections.Generic;
using UnityEngine;

public class MapPositioner : MonoBehaviour
{

    float sumOfDistance;

    List<MapPathPoint> MapPathPoints = new List<MapPathPoint>();

    private MapPathPoint firstMapPathPoint;
    private MapPathPoint lastMapPathPoint;

    public MapPathPoint FirstMapPathPoint { get => firstMapPathPoint; }
    public MapPathPoint LastMapPathPoint { get => lastMapPathPoint; }

    [SerializeField]
    bool debugOutput = false;

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

            if (mapPathPoint.NumberInPointSequence == 0)
            {
                firstPoint = mapPathPoint;
            }
        }
        MapPathPoints = new List<MapPathPoint>(sortedMapPathPoints);

        lastMapPathPoint = MapPathPoints[MapPathPoints.Count - 1];

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

    //TODO if there is time: Optimize this by doing a calculation of deltas, storing it in an array,
    // then using simple loops and if statement bounds to return the position
    /// <summary>
    /// </summary>
    /// <param name="progress">The percentage progress; 0.0 representing 0% and 1.0 representing 100%</param>
    /// <returns></returns>
    public Vector2 GetPositionOnMapByProgress(float progress)
    {
        if (progress >= 1.0f)
        {
            return lastMapPathPoint.transform.position;
        }

        //total distance along the line composed of all the linear lines
        float distance = sumOfDistance * progress;

        //represents a linear line between these two points
        MapPathPoint point1 = null;
        MapPathPoint point2 = null;

        if (debugOutput)
            Debug.Log("Distance and percentage: " + distance + ", " + progress);

        //Find the two points the progress is supposed to be in between 
        float progressRemaining = progress;
        float distanceRemaining = distance;
        for (int i = 0; i < MapPathPoints.Count - 1; i++)
        {

            if (MapPathPoints[i] == null)
            {
                Debug.LogError("Missing map path point " + i);
            }

            //this is the distance between the current point and the next, starting from point 
            float deltaDistance = Vector2.Distance(
                    MapPathPoints[i].gameObject.transform.position,
                    MapPathPoints[i + 1].gameObject.transform.position
                    );
            //if the remaining distance minus the distance between
            //this point and the next point is less than zero,
            //then the progress is between the following two points
            //Debug.Log(deltaDistance);
            if (debugOutput)
                Debug.Log("DeltaDistance: " + deltaDistance);
            distanceRemaining -= deltaDistance;
            if (distanceRemaining < 0)
            {
                point1 = MapPathPoints[i];
                point2 = MapPathPoints[i + 1];
                break;
            }
            progressRemaining -= deltaDistance / sumOfDistance;

        }

        //A nullreference will be thrown here if the above loop is not working correctly
        float distanceBetweenP1P2 = Vector2.Distance(point1.transform.position, point2.transform.position);

        if (debugOutput)
        {
            Debug.DrawLine(point1.transform.position, point2.transform.position);
            Debug.Log("1: " + point1.transform.position, point1);
            Debug.Log("2: " + point2.transform.position, point2);
            Debug.Log("Progress Remaining Delta: " + progressRemaining);
        }

        //line between point 1 and point 2
        Vector2 dir = point2.transform.position - point1.transform.position;
        dir.Normalize();
        dir *= sumOfDistance * progressRemaining;

        return dir + (Vector2)point1.transform.position;


    }


}
