using System.Collections;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float platformSpeed;
    [SerializeField] private float rotateAngle;
    [SerializeField] private float platformRotateDelay;
    [SerializeField] private bool rotateRight;
    private float rotateTime;
    private float rotationDirection;
    private bool isRotating = false;
    

    void Start()
    {
        rotateTime = rotateAngle / platformSpeed;
        if (rotateRight) {
            rotationDirection = -1;
        } else {
            rotationDirection = 1;
        }
        RotatePlatform();
    }

    void RotatePlatform()
    {
        StartCoroutine(RotateForTime(rotateTime));
    }

    void Update()
    {
        if (isRotating) {
            platform.transform.Rotate(0, 0,rotationDirection * (platformSpeed * Time.deltaTime));
        }
    }

    IEnumerator RotateForTime(float duration)
    {
        isRotating = true;
        yield return new WaitForSeconds(duration);
        StartCoroutine(Delay(platformRotateDelay));
    }

    IEnumerator Delay(float duration)
    {
        isRotating = false;
        yield return new WaitForSeconds(duration);
        StartCoroutine(RotateForTime(rotateTime));
    }
}
