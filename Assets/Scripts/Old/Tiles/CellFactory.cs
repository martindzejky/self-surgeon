using UnityEngine;

public class CellFactory : MonoBehaviour {
    public GameObject bloodCellPrefab;

    private float lastTimeSpawned = 0;

    public void Update() {
        var currentImunity = GlobalGameController.globalInstance.currentImunity;

        if (Random.value * 100000f < currentImunity) {
            if (Time.realtimeSinceStartup > this.lastTimeSpawned + 1) {
                var position = Random.insideUnitCircle.normalized;

                Instantiate(bloodCellPrefab, new Vector3(
                    transform.position.x + position.x,
                    transform.position.y + position.y,
                    0f
                ), Quaternion.identity);

                this.lastTimeSpawned = Time.realtimeSinceStartup;
            }
        }
    }
}
