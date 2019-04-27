using UnityEngine;

public class BodyPart : MonoBehaviour {
    public string partName;

    public void OnEnable() {
        this.partName = this.gameObject.name;
    }
}
