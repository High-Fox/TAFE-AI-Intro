using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class AIGuardPoints : MonoBehaviour
{
    [Tooltip("object that will be moving around")]
    public GameObject movingObject;
    [Tooltip("positions to move between (in order)")]
    public GameObject[] points;
    public GameObject player;
    [Tooltip("minimum distance to target point to move on")]
    public float minPositionDistance = 0.2f;
    [Tooltip("how close player has to be to chase them")]
    public float chaseDistance = 3f;
    public float speed = 2f;

    private int pointIndex = 0;

    void Update()
    {
        // if within certain distance to player, chase them and change the target point to whichever is nearest
        if (DistanceTo(player) < chaseDistance)
        {
            MoveTowards(player);
            // big brain method of getting nearest point
            GameObject nearestPoint = points.OrderBy(point => DistanceTo(point)).First();
            pointIndex = Array.IndexOf(points, nearestPoint);
        }
        else
        {
            GameObject targetPoint = points[pointIndex];
            MoveTowards(targetPoint);
            if (DistanceTo(targetPoint) < 0.2f)
            {
                CyclePointIndex();
            }
        }
    }

    private float DistanceTo(GameObject gameObject)
    {
        return Vector2.Distance(movingObject.transform.position, gameObject.transform.position);
    }

    private void MoveTowards(GameObject target)
    {
        // This isnt how we were told to do it but its better ;)
        Vector2 movedPos = Vector3.MoveTowards(movingObject.transform.position, target.transform.position, speed * Time.deltaTime);
        movingObject.transform.position = movedPos;
    }

    private void CyclePointIndex()
    {
        // Add 1 to the pointIndex, looping it back to 0 if it would be higher than the amount of positions
        if (++pointIndex > points.Length - 1)
        {
            pointIndex = 0;
        }
    }
}
