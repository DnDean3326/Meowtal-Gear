using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private MovingPlatform myMovingPlatform;
    [SerializeField] private float delay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.CompareTag("Player");
        Invoke(nameof(TriggerSignal), delay);
    }

    private void TriggerSignal() {
        myMovingPlatform.TriggerChange(true);
    }
}
