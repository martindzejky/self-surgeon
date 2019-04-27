using UnityEngine;
using UnityEngine.UI;

public class BodyPartInfoController : MonoBehaviour {
    public GameObject panel;
    public Text partName;
    public Text bloodText;
    public Text imunityText;

    public void Update() {
        bool enabled = PartHighlighter.instance.selectedBodyPart;

        if (enabled) {
            this.panel.SetActive(true);
            this.SetupPanel();
        } else {
            this.panel.SetActive(false);
        }
    }

    private void SetupPanel() {
        this.partName.text = PartHighlighter.instance.selectedBodyPart.partName;
    }
}
