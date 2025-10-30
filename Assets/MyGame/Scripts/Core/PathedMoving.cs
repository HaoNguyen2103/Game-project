using System;
using UnityEngine;
[AddComponentMenu("HaoNguyen/PathedMoving")]
public class PathedMoving : MonoBehaviour
{
    [Header("Waypoint")]
    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;
    [Header("Speed Moving")]
    public float speed;
    public bool cycle;
    public float waitTime;
    [Range(0, 2)]
    public float easeAmount;
    [Header("Size")]
    public float size = 0.3f;
    int fromWaypointIndex;
    float percentBetweenWaypoints;
    float nextMoveTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = CalculateMovement();
        transform.Translate(velocity);
    }
    private Vector3 CalculateMovement()
    {
        if (Time.time < nextMoveTime)
        {
            return Vector3.zero;
        }
        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;
        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoints);
        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);
        if (percentBetweenWaypoints >= 1)
        {
            percentBetweenWaypoints = 0;
            fromWaypointIndex++;
            if (!cycle)
            {
                if (fromWaypointIndex >= globalWaypoints.Length - 1)
                {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            }
            nextMoveTime = Time.time + waitTime;
        }
        return newPos - transform.position;
    }
    float Ease(float x)
    {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (localWaypoints != null)
        {
            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);

            }
        }
    }
}
