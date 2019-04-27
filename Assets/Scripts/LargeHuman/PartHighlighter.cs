using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class PartHighlighter : MonoBehaviour {
    public List<BodyPart> mouseOverParts = new List<BodyPart>();

    public Color highlightColor;

    public GameObject uiTextObject;

    [HideInInspector]
    public BodyPart currentOverPart;
    [HideInInspector]
    public BodyPart previousOverPart;

    [HideInInspector]
    public BodyPart selectedBodyPart;

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
            this.currentOverPart = this.mouseOverParts.Last();

            if (!this.previousOverPart || this.previousOverPart != this.currentOverPart) {
                Debug.Log("New body part under mouse " + this.currentOverPart.partName);
                this.previousOverPart = this.currentOverPart;
                this.currentOverPart.GetComponent<SpriteRenderer>().color = this.highlightColor;
            }
        } else {
            this.currentOverPart = null;
            this.previousOverPart = null;
        }

        if (this.currentOverPart) {
            this.uiTextObject.SetActive(true);

            var textComponent = this.uiTextObject.GetComponent<Text>();
            textComponent.text = this.currentOverPart.partName;
        } else {
            this.uiTextObject.SetActive(false);
        }

        this.HandleClicking();
    }

    private void HandleClicking() {
        if (Input.GetButtonDown("Primary")) {
            if (this.currentOverPart) {
                this.selectedBodyPart = this.currentOverPart;
            } else {
                this.selectedBodyPart = null;
            }
        }
    }
}
