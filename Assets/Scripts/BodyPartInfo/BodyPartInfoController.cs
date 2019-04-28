using UnityEngine;
using UnityEngine.UI;

public class BodyPartInfoController : MonoBehaviour {
    public GameObject panel;
    public Text partName;
    public Text bloodText;
    public Text imunityText;
    public Text missingText;
    public Button operateButton;
    public Text priceText;

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

        var humanDefinition = GlobalGameController
            .globalInstance
            .FindHumanPartDefinition(selectedPart.partName);

        var roboticDefinition = GlobalGameController
            .globalInstance
            .FindRoboticPartDefinition(selectedPart.partName);

        this.operateButton.gameObject.SetActive(selectedPart.currentType != BodyPartType.Robotic);

        if (selectedPart.currentType == BodyPartType.Missing) {
            this.missingText.enabled = true;
            this.partName.text = selectedPart.partName + " (missing)";

            if (roboticDefinition.bodyPartName != default(RoboticPartDefinition).bodyPartName) {
                this.priceText.text = "buy robotic part for $" + roboticDefinition.price;
            } else {
                Debug.LogWarning("Missing definition for body part " + selectedPart.partName);
            }
        } else {
            this.missingText.enabled = false;
        }

        if (selectedPart.currentType == BodyPartType.Human) {
            if (humanDefinition.bodyPartName != default(HumanPartDefinition).bodyPartName) {
                this.partName.text = humanDefinition.name;
                this.bloodText.enabled = true;
                this.imunityText.enabled = true;
                this.bloodText.text = "blood: " + humanDefinition.blood;
                this.imunityText.text = "imunity: " + humanDefinition.imunity;
                this.priceText.text = "worth $" + humanDefinition.price;
            } else {
                Debug.LogWarning("Missing definition for body part " + selectedPart.partName);
            }
        } else {
            this.bloodText.enabled = false;
            this.imunityText.enabled = false;
        }

        if (selectedPart.currentType == BodyPartType.Robotic) {
            if (roboticDefinition.bodyPartName != default(RoboticPartDefinition).bodyPartName) {
                this.partName.text = roboticDefinition.name;
                this.priceText.text = "worth $" + roboticDefinition.price;
            } else {
                Debug.LogWarning("Missing definition for body part " + selectedPart.partName);
            }
        } else {
        }
    }
}
