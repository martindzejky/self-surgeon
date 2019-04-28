using UnityEngine;

public class Syringe : MonoBehaviour {
    public Transform particleSpawn;

    private ToolManipulation tool;
    
    private float power = 10f;

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

        if (collider.gameObject.tag == "Tile") {
            var canGetHurt = collider.GetComponent<CanGetHurtTile>();
            if (canGetHurt) {
                canGetHurt.life += Time.deltaTime * this.power;
                GlobalGameController.globalInstance.currentBlood += Time.deltaTime * this.power / 15f;

                if (Random.value < .2f) {
                    ParticleController.instance.SpawnBloodParticles(particleSpawn.position);
                }
            }
        }
    }
}
