using UnityEngine;

public class Knife : MonoBehaviour {
    public Transform particleSpawn;

    private ToolManipulation tool;
    private float previousPositionX;
    private float previousPositionY;
    private float previousRotation;
    private float previousPositionX2;
    private float previousPositionY2;
    private float previousRotation2;

    private float power = 60f;

    public void Awake() {
        this.tool = this.GetComponent<ToolManipulation>();
    }

    public void FixedUpdate() {
        this.previousPositionX2 = this.previousPositionX;
        this.previousPositionY2 = this.previousPositionY;
        this.previousRotation2 = this.previousRotation;

        this.previousPositionX = this.transform.position.x;
        this.previousPositionY = this.transform.position.y;
        this.previousRotation = this.transform.localRotation.eulerAngles.z;
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (!this.DoesDamage()) return;

        if (collider.gameObject.tag == "Enemy") {
            var bloodCell = collider.gameObject.GetComponent<BloodCell>();
            if (bloodCell) {
                bloodCell.Die();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider) {
        if (!this.DoesDamage()) return;

        if (collider.gameObject.tag == "Tile") {
            var canGetHurt = collider.GetComponent<CanGetHurtTile>();
            if (canGetHurt) {
                canGetHurt.life -= Time.deltaTime * this.power;

                if (Random.value < .2f) {
                    ParticleController.instance.SpawnBloodParticles(particleSpawn.position);
                }
            }
        }
    }

    private bool DoesDamage() {
        if (!tool.lifted) return false;

        return (
            transform.position.x != this.previousPositionX2 ||
            transform.position.y != this.previousPositionY2 ||
            transform.rotation.eulerAngles.z != this.previousRotation2
        );
    }
}
