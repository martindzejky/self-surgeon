using UnityEngine;

public class Syringe : MonoBehaviour {
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
                bloodCell.movementSpeed *= 1.2f;
                bloodCell.movementSpeed = Mathf.Min(bloodCell.movementSpeed, 10f);
                ParticleController.instance.SpawnBloodParticles(particleSpawn.position);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider) {
        if (!tool.lifted) return;
    }
}
