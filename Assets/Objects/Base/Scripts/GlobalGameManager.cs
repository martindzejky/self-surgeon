using UnityEngine;

public class GlobalGameManager : MonoBehaviour {
    // static instance
    public static GlobalGameManager instance {
        get;
        private set;
    }

    public void Awake() {
        if (GlobalGameManager.instance) {
            Destroy(this.gameObject);
            return;
        } 

        GlobalGameManager.instance = this;
    }
}
