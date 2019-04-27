using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameController : MonoBehaviour {
    public static GlobalGameController globalInstance {
        get;
        private set;
    }

    public HumanPartDefinition[] bodyPartDatabase;
    public RoboticPartDefinition[] roboticPartDatabase;

    public List<BodyPartGlobalState> currentPlayerBodyState; // probably should be a hash set

    public HumanPartDefinition FindHumanPartDefinition(string partName) {
        return this
            .bodyPartDatabase
            .FirstOrDefault(
                part => part.bodyPartName == partName
            );
    }

    public RoboticPartDefinition FindRoboticPartDefinition(string partName) {
        return this
            .roboticPartDatabase
            .FirstOrDefault(
                part => part.bodyPartName == partName
            );
    }

    public void OnOperateClick() {
        var currentlySelectedPart = PartHighlighter.instance.selectedBodyPart;

        if (currentlySelectedPart.currentType == BodyPartType.Robotic) return;

        var partDefinition = this.FindHumanPartDefinition(currentlySelectedPart.partName);

        if (partDefinition.operationScene != null && partDefinition.operationScene != "") {
            Debug.Log("Opening operation scene " + partDefinition.operationScene);
            SceneManager.LoadScene("Scenes/Operations/" + partDefinition.operationScene);
        }
    }

    public void Awake() {
        if (!this.InitializeInstance()) return;
    }

    private bool InitializeInstance() {
        if (!GlobalGameController.globalInstance) {
            GlobalGameController.globalInstance = this;
        } else if (GlobalGameController.globalInstance != this) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        return true;
    }
}
