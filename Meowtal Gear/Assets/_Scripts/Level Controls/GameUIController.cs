using UnityEngine;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    private PlayerController playerController;
    private UIDocument myUIDoc;
    private Label myHealthLabel;
    private VisualElement myHealthBarMask;

    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        myUIDoc = GetComponent<UIDocument>();
    }
    
    private void Start()
    {
        Link();
    }

    public void Link()
    {
        myHealthLabel = myUIDoc.rootVisualElement.Q<Label>("HealthLabel");
        myHealthBarMask = myUIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
    }

    private void Update()
    {
        HealthTrack();
    }

    private void HealthTrack()
    {
        myHealthLabel.text = $"{playerController.GetHealth()}/{playerController.GetMaxHealth()}";

        float healthRatio = playerController.GetHealth() / playerController.GetMaxHealth();
        float healthPercent = Mathf.Lerp(25, 85, healthRatio);
        myHealthBarMask.style.width = Length.Percent(healthPercent);
    }
}
