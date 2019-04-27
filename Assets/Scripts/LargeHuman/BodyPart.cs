using UnityEngine;

public class BodyPart : MonoBehaviour {
    public string partName;
    public BodyPartType currentType;

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
}
