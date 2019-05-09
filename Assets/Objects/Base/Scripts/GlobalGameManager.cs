using UnityEngine;

/**
 * An instance of this component should be always loaded in the game.
 * It is in the ManagerScene.
 */
public class GlobalGameManager : MonoBehaviour {
    // static instance
    public static GlobalGameManager instance {
        get;
        private set;
    }

    private void Awake() {
        if (GlobalGameManager.instance) {
            Destroy(this.gameObject);
            return;
        } 

        GlobalGameManager.instance = this;
    }
}
