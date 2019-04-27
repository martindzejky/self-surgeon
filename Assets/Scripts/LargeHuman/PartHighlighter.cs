using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartHighlighter : MonoBehaviour {
    public Stack<BodyPart> mouseOverParts = new Stack<BodyPart>();

    public GameObject uiTextObject;

    [HideInInspector]
    public BodyPart currentOverPart;
    [HideInInspector]
    public BodyPart previousOverPart;

    private Camera mainCamera;

    public static PartHighlighter main {
        get;
        private set;
    }

    public void OnEnable() {
        PartHighlighter.main = this;
        this.mainCamera = Camera.main;
    }

    public void Update() {
        if (this.mouseOverParts.Count > 0) {
            this.currentOverPart = this.mouseOverParts.Peek();

            if (!this.previousOverPart || this.previousOverPart != this.currentOverPart) {
                Debug.Log("New body part under mouse " + this.currentOverPart.partName);
                this.previousOverPart = this.currentOverPart;
            }
        } else {
            this.currentOverPart = null;
            this.previousOverPart = null;
        }

        if (this.currentOverPart) {
            this.uiTextObject.SetActive(true);

            var textComponent = this.uiTextObject.GetComponent<Text>();
            textComponent.text = this.currentOverPart.partName;

            this.HandleClicking();
        } else {
            this.uiTextObject.SetActive(false);
        }
    }

    private void HandleClicking() {
    }
}
