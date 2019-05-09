using UnityEngine;

public class BodyPart : MonoBehaviour {
    public string partName;
    public BodyPartType currentType;

    public BodyPart requiredPart;

    public Sprite humanSprite;
    public Sprite roboticSprite;

    public void InitializeBasedOnCurrentType() {
        var spriteRenderer = this.GetComponent<SpriteRenderer>();

        switch (this.currentType) {
            case BodyPartType.Human:
                spriteRenderer.sprite = this.humanSprite;
                break;

            case BodyPartType.Robotic:
                spriteRenderer.sprite = this.roboticSprite;
                break;
            
            default:
                spriteRenderer.sprite = null;
                break;
        }
    }

    public void Awake() {
        if (this.requiredPart && this.requiredPart.currentType == BodyPartType.Missing) {
            this.currentType = BodyPartType.Missing;
            this.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
