using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SuccessText : MonoBehaviour {
    private Text sprite;

    public void Awake() {
        this.sprite = this.GetComponent<Text>();

        var currentColor = this.sprite.color;
        currentColor.a = 0;
        this.sprite.color = currentColor;
    }

    public void Update() {
        if (GlobalGameController.globalInstance.currentGoals.Count == 0) {
            var currentColor = this.sprite.color;
            currentColor.a = Mathf.Clamp01(currentColor.a + Time.deltaTime * 2f);
            this.sprite.color = currentColor;
        }
    }

}
