using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleSO", menuName = "Scriptable Objects/CollectibleSO")]
public class CollectibleSO : ScriptableObject
{
    [SerializeField] private string collectibleType;
    [SerializeField] private float valueChange;
    [SerializeField] private float duration;

    public string GetCollectibleType()
    {
        return collectibleType;
    }
    
    public float GetValueChange()
    {
        return valueChange;
    }

    public float GetDuration()
    {
        return duration;
    }
}
