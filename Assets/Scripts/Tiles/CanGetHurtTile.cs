using UnityEngine;

public class CanGetHurtTile : MonoBehaviour {
    public float maxLife = 100f;
    public float life = 100f;
    public bool isGoal = false;

    private SpriteRenderer spriteRenderer;
    private Color goalColor = new Color(.5f, 0, 0);

    public void Start() {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        if (this.isGoal) {
            GlobalGameController.globalInstance.currentGoals.Add(this);
        }
    }

    public void Update() {
        if (this.isGoal) {
            var value = Mathf.Sin(Time.timeSinceLevelLoad * 3f) / 2f + .5f;
            this.spriteRenderer.color = Color.Lerp(Color.white, this.goalColor, value);
        } else {
            this.spriteRenderer.color = Color.Lerp(Color.gray, Color.white, this.life / this.maxLife);
        }

        if (this.life <= 0) {
            if (this.isGoal) {
                GlobalGameController.globalInstance.CompleteGoal(this);
            }

            ParticleController.instance.SpawnKillParticles(this.transform.position, 4);
            Destroy(this.gameObject);
        }
    }
}
