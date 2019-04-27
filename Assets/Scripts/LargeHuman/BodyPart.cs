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
        if (!PartHighlighter.main.mouseOverParts.Contains(this)) {
            PartHighlighter.main.mouseOverParts.Add(this);
        }
    }

    public void OnMouseExit() {
        PartHighlighter.main.mouseOverParts.Remove(this);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
