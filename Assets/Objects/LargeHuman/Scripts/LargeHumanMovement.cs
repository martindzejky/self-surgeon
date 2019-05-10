using UnityEngine;

public class LargeHumanMovement : MonoBehaviour {
    public float maxMovementSpeed;
    public float maxAcceleration;
    public float deceleration;

    [HideInInspector]
    public float currentMovementSpeed;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        var currentInput = Input.GetAxisRaw("Horizontal");
        var absCurrentInput = Mathf.Abs(currentInput);

        if (absCurrentInput > .01f) {
            var currentAcceleration = maxAcceleration * currentInput;

            currentMovementSpeed = Mathf.Clamp(
                currentMovementSpeed + currentAcceleration,
                -maxMovementSpeed,
                maxMovementSpeed
            );
        } else {
            var newAbsMovementSpeed = Mathf.Abs(currentMovementSpeed) - deceleration;
            newAbsMovementSpeed = Mathf.Max(0, newAbsMovementSpeed);

            currentMovementSpeed = newAbsMovementSpeed * Mathf.Sign(currentMovementSpeed);
        }

        var currentMovementPercentage = currentMovementSpeed / maxMovementSpeed;
        var absCurrentMovementPercentage = Mathf.Abs(currentMovementPercentage);

        if (absCurrentMovementPercentage > .001f) {
            var currentPosition = transform.position;
            currentPosition.x += currentMovementSpeed * Time.deltaTime;
            transform.position = currentPosition;

            var currentScale = transform.localScale;
            currentScale.x = -Mathf.Sign(currentMovementSpeed);
            transform.localScale = currentScale;
        }

        if (animator) {
            animator.SetFloat("movementSpeed", absCurrentMovementPercentage);
        }
    }
}
