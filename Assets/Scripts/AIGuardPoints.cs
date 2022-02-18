using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("how close player has to be to the current point to chase them")]
    public float chaseDistance = 3f;
    public float speed = 2f;

    private int pointIndex = 0;

    void Update()
    {
        GameObject targetPoint = points[pointIndex];

        if (Vector2.Distance(targetPoint.transform.position, player.transform.position) < chaseDistance)
        {
            MoveTowards(player.transform);
        }
        else
        {
            MoveTowards(targetPoint.transform);
        }
    }

    private void MoveTowards(Transform target)
    {
        // This isnt how we were told to do it but its better ;)
        Vector2 movedPos = Vector3.MoveTowards(movingObject.transform.position, target.position, speed * Time.deltaTime);
        movingObject.transform.position = movedPos;

        if (Vector2.Distance(movedPos, target.position) < 0.2f)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        // Add 1 to the positionIndex, looping it back to 0 if it would be higher than the amount of positions
        pointIndex = (int)Mathf.Repeat(pointIndex + 1, points.Length);
    }
}
