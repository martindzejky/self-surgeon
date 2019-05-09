using UnityEngine;

[CreateAssetMenu(menuName = "Surgeon/Body part")]
public class BodyPartAsset : ScriptableObject {
    public BodyPartAssetType bodyPartType;
    public string bodyPartName;
}
