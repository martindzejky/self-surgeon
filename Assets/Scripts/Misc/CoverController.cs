using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CoverController : MonoBehaviour {
    public static CoverController instance {
        get;
        private set;
    }

    private SpriteRenderer sprite;
    private float currentAlpha;
    private float targetAlpha;

    public void Awake() {
        if (CoverController.instance) {
            Destroy(this.gameObject);
            return;
        }

        CoverController.instance = this;
        this.sprite = this.GetComponent<SpriteRenderer>();

        this.FadeIn();
    }

    public void Update() {
        if (Mathf.Abs(this.currentAlpha - this.targetAlpha) > float.Epsilon) {
            var dir = Mathf.Sign(this.targetAlpha - this.currentAlpha);
            this.currentAlpha += dir * Time.deltaTime * 4f;
            this.currentAlpha = Mathf.Clamp01(this.currentAlpha);

            var currentColor = this.sprite.color;
            currentColor.a = this.currentAlpha;
            this.sprite.color = currentColor;
        }
    }

    public void FadeIn() {
        Debug.Log("Fading in");
        this.currentAlpha = 1f;
        this.targetAlpha = 0f;
    }

    public void FadeOut() {
        Debug.Log("Fading out");
        this.currentAlpha = 0f;
        this.targetAlpha = 1f;
    }
}
