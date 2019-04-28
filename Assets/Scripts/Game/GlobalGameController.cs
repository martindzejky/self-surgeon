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

    public BodyPart currentlyOperatingBodyPart;
    public GameObject currentPlayer;
    public List<CanGetHurtTile> currentGoals = new List<CanGetHurtTile>();

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

    public void StartOperation() {
        var currentlySelectedPart = PartHighlighter.instance.selectedBodyPart;

        if (currentlySelectedPart.currentType == BodyPartType.Robotic) return;

        this.currentlyOperatingBodyPart = currentlySelectedPart;
        switch (currentlySelectedPart.partName) {
            case "Upper Left Arm":
            case "Lower Left Arm":
            case "Upper Right Arm":
            case "Lower Right Arm":
                SceneManager.LoadScene("ArmOperation");
                break;

            default:
                Debug.LogWarning("Operation level not defined for body part " + currentlySelectedPart.partName);
                break;
        }
    }

    public void KillPlayer() {
        if (!this.currentPlayer) return;

        Debug.Log("Killing the player");

        ParticleController.instance.SpawnKillParticles(this.currentPlayer.transform.position, 5);
        Destroy(this.currentPlayer);
    }

    public void CompleteGoal(CanGetHurtTile goal) {
        if (!goal.isGoal) return;

        this.currentGoals.Remove(goal);

        Debug.Log("Goal completed " + goal.gameObject.name);
        Debug.Log("Remaining goals " + this.currentGoals.Count);

        if (this.currentGoals.Count == 0) {
            SceneManager.LoadScene("BodyPartSelectScene");
        }
    }

    public void Awake() {
        if (!this.InitializeInstance()) return;

        SceneManager.activeSceneChanged += this.InitializeInNewScene;
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

    private void InitializeInNewScene(Scene _, Scene newScene) {
        if (newScene.name == "BodyPartSelectScene") {
            this.InitializeInBodyPartSelectScene();
        } else {
            this.InitializeInOperationScene();
        }
    }

    private void InitializeInBodyPartSelectScene() {
        this.currentGoals.Clear();
        this.currentPlayer = null;
    }

    private void InitializeInOperationScene() {
        this.currentPlayer = GameObject.FindWithTag("Player");

        Debug.Log("Registered goals " + this.currentGoals.Count);
    }
}
