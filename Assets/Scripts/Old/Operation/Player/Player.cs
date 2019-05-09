using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject carriedTool;

    private float movementSpeed = 7f;

    private Rigidbody2D physicsBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public void Awake() {
        this.physicsBody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void Update() {
        var deltaTime = Time.deltaTime;

        this.Move(deltaTime);
    }

    private void Move(float deltaTime) {
        var inputVector = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0f
        );

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
