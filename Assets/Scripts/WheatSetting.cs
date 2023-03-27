using UnityEngine;
[CreateAssetMenu(menuName = "Settings/Wheat")]
public class WheatSetting : ScriptableObject
{
    [SerializeField] private float growTime;
    [SerializeField] private float forceBlock;
    public float GrowTime => growTime;
    public float ForceBlock => forceBlock;
}
