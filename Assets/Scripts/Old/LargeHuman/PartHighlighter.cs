using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class PartHighlighter : MonoBehaviour {
    public Color highlightColor;

    public GameObject uiTextObject;

    public BodyPart previousOverPart;
    public BodyPart currentOverPart;
    public BodyPart selectedBodyPart;

    private Camera mainCamera;

    public static PartHighlighter instance {
        get;
        private set;
    }

    public void OnEnable() {
        PartHighlighter.instance = this;
        this.mainCamera = Camera.main;
    }

    public void Update() {
        this.currentOverPart = null;

        var mousePosition = this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        if (hits.Length > 0) {
            var filteredHits = hits
                .Where(hit => hit.collider.GetComponent<BodyPart>())
                .OrderBy(hit => {
                    var renderer = hit.collider.GetComponent<SpriteRenderer>();
                    var bodyPart = hit.collider.GetComponent<BodyPart>();
                    
                    if (renderer) {
                        return renderer.sortingOrder;
                    } else {
                        Debug.LogWarning("Missing sprite renderer on body part " + bodyPart.partName);
                        return int.MinValue;
                    }
                });

            if (filteredHits.Count() > 0) {
                this.currentOverPart = filteredHits.Last().collider.GetComponent<BodyPart>();
            }
        }

        if (this.currentOverPart) {
            this.SetBodyPartColor(this.currentOverPart, this.highlightColor);

            this.uiTextObject.SetActive(true);

            var textComponent = this.uiTextObject.GetComponent<Text>();
            textComponent.text = this.currentOverPart.partName;

            if (this.currentOverPart.currentType == BodyPartType.Missing) {
                textComponent.text += " (missing)";
            }
            
            var transform = this.uiTextObject.GetComponent<RectTransform>();
            transform.position = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                0f
            );
        } else {
            this.uiTextObject.SetActive(false);
        }

        this.HandleClicking();

        if (
            this.currentOverPart &&
            this.currentOverPart != this.previousOverPart
        ) {
        }

        if (
            this.previousOverPart &&
            this.previousOverPart != this.currentOverPart
        ) {
            this.SetBodyPartColor(this.previousOverPart, Color.white);
        }
    }

    private void LateUpdate() {
        this.previousOverPart = this.currentOverPart;
    }

    private void HandleClicking() {
        if (Input.GetButtonDown("Primary")) {
            if (this.currentOverPart) {
                this.selectedBodyPart = this.currentOverPart;
            } else {
                // this.selectedBodyPart = null;
            }
        }
    }

    private void SetBodyPartColor(BodyPart part, Color color) {
            var renderer = part.GetComponent<SpriteRenderer>();
            if (renderer) {
                renderer.color = color;
            } else {
                Debug.LogWarning("Missing sprite renderer on body part " + part.partName);
            }
    }
}
