using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameController : MonoBehaviour {
    public static GlobalGameController globalInstance {
        get;
        private set;
    }

    public HumanPartDefinition[] bodyPartDatabase;
    public RoboticPartDefinition[] roboticPartDatabase;

    public void OnOperateClick() {
        var currentlySelectedPart = PartHighlighter.main.selectedBodyPart;
        var partDefinition = this.bodyPartDatabase.First(part => part.name == currentlySelectedPart.partName);

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
