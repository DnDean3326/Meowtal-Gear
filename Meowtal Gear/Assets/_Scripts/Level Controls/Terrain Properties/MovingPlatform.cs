using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float platformSpeed;
    [SerializeField] private Transform[] points;
    [SerializeField] private int pointSelection;
    [SerializeField] private float platformMoveDelay;
    private bool isMoving = true;
    private Transform currentPoint;
    [SerializeField] private bool isTriggered = false;

    private void Start()
    {
        currentPoint = points[pointSelection];
    }

    private void Update()
    {
        if (isTriggered)
        {
            PlatformMovement();
        }
    }
    
    public void TriggerChange(bool response)
    {
        isTriggered = response;
    }

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
