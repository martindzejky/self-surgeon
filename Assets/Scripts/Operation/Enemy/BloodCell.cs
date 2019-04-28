using UnityEngine;

public class BloodCell : MonoBehaviour {
    private float movementSpeed = 2f;

    private Rigidbody2D physicsBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Player player;

    public void Awake() {
        this.physicsBody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void Update() {
        var deltaTime = Time.deltaTime;

        this.Move(deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            GlobalGameController.globalInstance.KillPlayer();
        }
    }

    public void Die() {
        Destroy(this.gameObject);
    }

    private void Move(float deltaTime) {
        var inputVector = Vector3.zero;

        if (this.player) {
            inputVector = (this.player.transform.position - this.transform.position).normalized;
        }

        var deltaPosition = inputVector * deltaTime * this.movementSpeed;
        var magnitude = deltaPosition.magnitude;
        var targetPosition = this.transform.position + deltaPosition;

        this.physicsBody.MovePosition(targetPosition);
        this.animator.SetFloat("MovingSpeed", magnitude);

        if (Mathf.Abs(magnitude) > .1f) {
            this.spriteRenderer.flipX = deltaPosition.x < 0;
        }
    }
}
