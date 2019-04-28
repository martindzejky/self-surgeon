using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFinish : MonoBehaviour {
    public void GoToNextScene() {
        SceneManager.LoadScene("BodyPartSelectScene");
    }
    
    public void Update() {
        if (Input.GetButtonDown("Cancel")) {
            this.GoToNextScene();
        }
    }
}
