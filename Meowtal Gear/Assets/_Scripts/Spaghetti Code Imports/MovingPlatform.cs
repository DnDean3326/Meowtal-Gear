using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool isTriggered;
    [SerializeField] private GameObject platform;
    [SerializeField] private float platformSpeed;
    [SerializeField] private Transform[] points;
    [SerializeField] private int pointSelection;
    [SerializeField] private float platformMoveDelay;
    private bool isMoving = true;
    private Transform currentPoint;

    private void Start()
    {
        currentPoint = points[pointSelection];
    }

    private void Update()
    {
        if (isTriggered) {
            PlatformMovement();
        }
    }

    public void TriggerChange(bool response)
    {
        isTriggered = response;
    }

    /*IEnumerator PlatformMoveDelay(float delay)
    {
        Debug.Log($"Started at {Time.time}, waiting for {delay} seconds");
        yield return new WaitForSeconds(delay);
        Debug.Log($"Ended at {Time.time}"); 
    }*/

    private void MakeMove()
    {
        isMoving = true;
    }
    
    void PlatformMovement()
    {
        if (isMoving)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position,
                Time.deltaTime * platformSpeed);
        }
        if (platform.transform.position == currentPoint.position) {
            isMoving = false;
            Invoke(nameof(MakeMove), platformMoveDelay);
            pointSelection++;
            if (pointSelection == points.Length) {
                pointSelection = 0;
            }
            currentPoint = points[pointSelection];
        }
    }
}