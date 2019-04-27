using UnityEngine;
using UnityEngine.UI;

public class BodyPartInfoController : MonoBehaviour {
    public GameObject panel;
    public Text partName;
    public Text bloodText;
    public Text imunityText;

    [SerializeField]
    private bool previouslyEnabled = true;

    public void Update() {
        bool enabled = PartHighlighter.main.selectedBodyPart;

        if (this.previouslyEnabled != enabled) {
            if (enabled) {
                this.panel.SetActive(true);

                this.SetupPanel();
            } else {
                this.panel.SetActive(false);
            }
        }

        this.previouslyEnabled = enabled;
    }

    private void SetupPanel() {
        this.partName.text = PartHighlighter.main.selectedBodyPart.partName;
    }
}
