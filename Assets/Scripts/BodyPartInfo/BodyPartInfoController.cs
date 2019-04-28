using UnityEngine;
using UnityEngine.UI;

public class BodyPartInfoController : MonoBehaviour {
    public GameObject panel;
    public Text partName;
    public Text bloodText;
    public Text imunityText;
    public Text missingText;
    public Button operateButton;

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
        var selectedPart = PartHighlighter.instance.selectedBodyPart;

        this.operateButton.gameObject.SetActive(selectedPart.currentType != BodyPartType.Robotic);

        if (selectedPart.currentType == BodyPartType.Missing) {
            this.missingText.enabled = true;
            this.partName.text = selectedPart.partName + " (missing)";
        } else {
            this.missingText.enabled = false;
        }

        if (selectedPart.currentType == BodyPartType.Human) {
            var definition = GlobalGameController
                .globalInstance
                .FindHumanPartDefinition(selectedPart.partName);

            if (definition.bodyPartName != default(HumanPartDefinition).bodyPartName) {
                this.partName.text = definition.name;
                this.bloodText.enabled = true;
                this.imunityText.enabled = true;
                this.bloodText.text = "blood: " + definition.blood;
                this.imunityText.text = "imunity: " + definition.imunity;
            } else {
                Debug.LogWarning("Missing definition for body part " + selectedPart.partName);
            }
        } else {
            this.bloodText.enabled = false;
            this.imunityText.enabled = false;
        }

        if (selectedPart.currentType == BodyPartType.Robotic) {
            var definition = GlobalGameController
                .globalInstance
                .FindRoboticPartDefinition(selectedPart.partName);

            if (definition.bodyPartName != default(RoboticPartDefinition).bodyPartName) {
                this.partName.text = definition.name;
            } else {
                Debug.LogWarning("Missing definition for body part " + selectedPart.partName);
            }
        } else {
        }
    }
}
