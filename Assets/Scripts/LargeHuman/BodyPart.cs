using UnityEngine;

public class BodyPart : MonoBehaviour {
    public string partName;

    private BoxCollider2D boxCollider;

    public void OnEnable() {
        this.partName = this.gameObject.name;

        var collider = this.GetComponent<BoxCollider2D>();
        if (collider) {
            this.boxCollider = collider;
            Debug.Log("Collider found for " + this.partName);
        }
    }

    public void OnMouseEnter() {
        PartHighlighter.main.mouseOverParts.Push(this);
    }

    public void OnMouseExit() {
        PartHighlighter.main.mouseOverParts.Pop();
    }
}
