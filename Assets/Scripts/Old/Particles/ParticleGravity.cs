using UnityEngine;

public class ParticleGravity : MonoBehaviour {
    private float xDelta;
    private float yDelta;
    private float startingY;

    public void OnEnable() {
        this.xDelta = Random.Range(-10f, 10f);
        this.yDelta = Random.Range(4f, 12f);
        this.startingY = this.transform.position.y;
    }

    public void Update() {
        var deltaTime = Time.deltaTime;

        this.transform.position = new Vector3(
            this.transform.position.x + this.xDelta * deltaTime,
            this.transform.position.y + this.yDelta * deltaTime,
            0
        );

        if (this.transform.position.y <= this.startingY) {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Floor";
            if (Random.value < .2f) ParticleController.instance.DestroyBloodParticle(this.gameObject);
            Destroy(this);
        }
    }

    public void FixedUpdate() {
        this.xDelta *= .9f;
        this.yDelta -= .7f;
    }
}
