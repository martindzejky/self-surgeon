using UnityEngine;

public class Knife : MonoBehaviour {
    public Transform particleSpawn;

    private ToolManipulation tool;

    public void Awake() {
        this.tool = this.GetComponent<ToolManipulation>();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (!tool.lifted) return;

        if (collider.gameObject.tag == "Enemy") {
            var bloodCell = collider.gameObject.GetComponent<BloodCell>();
            if (bloodCell) {
                bloodCell.Die();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider) {
        if (!tool.lifted) return;

        if (collider.gameObject.tag == "Tile") {
            if (collider.GetComponent<CanGetHurtTile>()) {
                if (Random.value < .2f) {
                    ParticleController.instance.SpawnBloodParticles(particleSpawn.position);
                }
            }
        }
    }
}
