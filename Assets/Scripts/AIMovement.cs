using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIMovement : MonoBehaviour
{
    [Tooltip("position(s) to move between (in order)")]
    //public GameObject[] points;
    public List<GameObject> points;
    public GameObject player;
    public GameObject pointPrefab;
    [Tooltip("minimum distance to target point to move on")]
    public float minPositionDistance = 0.1f;
    [Tooltip("how close the player has to be to chase them")]
    public float chaseDistance = 3f;
    public float speed = 2f;

    public int pointIndex = 0;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            MakeRandomPoint();
        }
    }

    public void MakeRandomPoint()
    {
        Vector2 randPos = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));
        GameObject newPoint = Instantiate(pointPrefab, randPos, Quaternion.identity);
        points.Add(newPoint);
    }

    public bool ShouldAttack()
    {
        return DistanceTo(player) < chaseDistance;
    }

    public void SetPointToNearest()
    {
        GameObject nearestPoint = points.OrderBy(point => DistanceTo(point)).First();
        pointIndex = points.IndexOf(nearestPoint);
    }

    public void DestroyCurrentPoint()
    {
        GameObject currentPoint = points[pointIndex];
        points.Remove(currentPoint);
        Destroy(currentPoint);
    }

    public bool MoveToPoints()
    {
        GameObject currentPoint = points[pointIndex];
        MoveTowards(currentPoint);

        return DistanceTo(currentPoint) < minPositionDistance;

        /*if (DistanceTo(currentPoint) < minPositionDistance)
        {
           NextPoint();
        }*/
    }

    public void MoveAway(GameObject target)
    {
        Vector2 movedPos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.position = -movedPos;
    }

    public void MoveTowards(GameObject target)
    {
        Vector2 movedPos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.position = movedPos;
    }

    // Add 1 to the pointIndex, looping it back to 0 if it would be higher than the amount of points
    public void NextPoint()
    {
        if (++pointIndex > points.Count - 1)
        {
            pointIndex = 0;
        }
    }

    private float DistanceTo(GameObject gameObject)
    {
        return Vector2.Distance(transform.position, gameObject.transform.position);
    }
}
