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

    public List<BodyPartGlobalState> currentPlayerBodyState = new List<BodyPartGlobalState>(10); // probably should be a hash set

    public BodyPartGlobalState currentlyOperatingBodyPart;
    public GameObject currentPlayer;

    public List<CanGetHurtTile> currentGoals = new List<CanGetHurtTile>(10);

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

        this.currentlyOperatingBodyPart = this
            .currentPlayerBodyState
            .FirstOrDefault(part => part.bodyPartName == currentlySelectedPart.partName);

        switch (currentlySelectedPart.partName) {
            case "Upper Left Arm":
            case "Lower Left Arm":
            case "Upper Right Arm":
            case "Lower Right Arm":
                this.MoveIntoOperationScene("ArmOperation");
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
            this.MoveIntoBodyPartSelectScene();
        }
    }

    public void Awake() {
        if (!this.InitializeInstance()) return;

        SceneManager.activeSceneChanged += this.OnSceneLoad;
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

    private void MoveIntoBodyPartSelectScene() {
        var wasSuccessfull = this.currentGoals.Count == 0;

        this.currentGoals.Clear();
        this.currentPlayer = null;

        if (this.currentlyOperatingBodyPart.bodyPartName != default(BodyPartGlobalState).bodyPartName) {
            if (wasSuccessfull) {
                if (this.currentlyOperatingBodyPart.currentType == BodyPartType.Missing) {
                    Debug.LogError("IMPLEMENT ME");
                } else if (this.currentlyOperatingBodyPart.currentType == BodyPartType.Human) {
                    try {
                        var index = this.currentPlayerBodyState.FindIndex(part => part.bodyPartName == this.currentlyOperatingBodyPart.bodyPartName);
                        var partState = this.currentPlayerBodyState[index];
                        
                        this.currentPlayerBodyState.RemoveAt(index);

                        partState.currentType = BodyPartType.Missing;
                        this.currentPlayerBodyState.Add(partState);
                    }
                    catch {
                        Debug.Log("Missing body part state " + this.currentlyOperatingBodyPart.bodyPartName);
                    }
                }
            }
        }

        SceneManager.LoadScene("BodyPartSelectScene");
    }

    private void MoveIntoOperationScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoad(Scene _, Scene loadedScene) {
        this.currentPlayer = GameObject.FindWithTag("Player");
    }
}
