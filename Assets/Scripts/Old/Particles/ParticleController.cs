using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ParticleController : MonoBehaviour {
    public static ParticleController instance {
        get;
        private set;
    }

    public Transform particleContainer;
    public GameObject bloodParticlePrefab;

    private Stack<GameObject> bloodParticlePool = new Stack<GameObject>();

    public void Awake() {
        if (ParticleController.instance) {
            Destroy(this.gameObject);
            return;
        }

        ParticleController.instance = this;
    }

    public void SpawnBloodParticles(Vector2 position, float multiplier = 1f) {
        var staticToSpawn = 1 * multiplier + Random.Range(-1, 1);
        var movingToSpawn = 2 * multiplier + Random.Range(-1, 1);

        if (staticToSpawn > 0)
        for (var i = 0; i < staticToSpawn; i++) {
            var posDelta = Random.insideUnitCircle * .7f;
            var particle = this.MakeBloodParticle();
            particle.transform.localScale = new Vector3(Random.Range(.9f, 1.2f), Random.Range(.9f, 1.2f), 1);
            particle.transform.position = new Vector3(position.x + posDelta.x, position.y + posDelta.y, 0f);
            particle.GetComponent<SpriteRenderer>().sortingLayerName = "Floor";
        }

        if (movingToSpawn > 0)
        for (var i = 0; i < movingToSpawn; i++) {
            var posDelta = Random.insideUnitCircle * .7f;
            var particle = this.MakeBloodParticle();
            particle.transform.localScale = new Vector3(Random.Range(.9f, 1.2f), Random.Range(.9f, 1.2f), 1);
            particle.transform.position = new Vector3(position.x + posDelta.x, position.y + posDelta.y, 0f);
            particle.GetComponent<SpriteRenderer>().sortingLayerName = "Particles";
            particle.AddComponent<ParticleGravity>();
        }
    }

    public void SpawnKillParticles(Vector2 position, float multiplier = 1f) {
        var bigToSpawn = 1 * (multiplier / 2) + Random.Range(-1, 1);
        var staticToSpawn = 8 * multiplier + Random.Range(-1, 1);
        var movingToSpawn = 17 * multiplier + Random.Range(-1, 1);

        if (bigToSpawn > 0)
        for (var i = 0; i < bigToSpawn; i++) {
            var bigPosDelta = Random.insideUnitCircle * .1f;
            var bigParticle = this.MakeBloodParticle();
            bigParticle.transform.localScale = new Vector3(Random.Range(5f, 7f), Random.Range(3f, 5f), 1);
            bigParticle.transform.position = new Vector3(position.x + bigPosDelta.x, position.y + bigPosDelta.y, 0f);
            bigParticle.GetComponent<SpriteRenderer>().sortingLayerName = "Floor";
        }

        if (staticToSpawn > 0)
        for (var i = 0; i < staticToSpawn; i++) {
            var posDelta = Random.insideUnitCircle * .7f;
            var particle = this.MakeBloodParticle();
            particle.transform.localScale = new Vector3(Random.Range(.9f, 1.2f), Random.Range(.9f, 1.2f), 1);
            particle.transform.position = new Vector3(position.x + posDelta.x, position.y + posDelta.y, 0f);
            particle.GetComponent<SpriteRenderer>().sortingLayerName = "Floor";
        }

        if (movingToSpawn > 0)
        for (var i = 0; i < movingToSpawn; i++) {
            var posDelta = Random.insideUnitCircle * .7f;
            var particle = this.MakeBloodParticle();
            particle.transform.localScale = new Vector3(Random.Range(.9f, 1.2f), Random.Range(.9f, 1.2f), 1);
            particle.transform.position = new Vector3(position.x + posDelta.x, position.y + posDelta.y, 0f);
            particle.GetComponent<SpriteRenderer>().sortingLayerName = "Particles";
            particle.AddComponent<ParticleGravity>();
        }
    }

    public void DestroyBloodParticle(GameObject particle) {
        particle.SetActive(false);

        this.bloodParticlePool.Push(particle);
    }

    private GameObject MakeBloodParticle() {
        GameObject instance;

        if (this.bloodParticlePool.Count > 0) {
            instance = bloodParticlePool.Pop();
        } else {
            instance = Instantiate(this.bloodParticlePrefab);
        }

        instance.transform.parent = this.particleContainer;
        instance.SetActive(true);

        return instance;
    }
}
