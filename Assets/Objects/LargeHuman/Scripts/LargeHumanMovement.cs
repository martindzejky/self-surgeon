using UnityEngine;

public class LargeHumanMovement : MonoBehaviour {
    public float movementSpeed;

    private Animator animator;

    private void Awake() {
        this.animator = this.GetComponent<Animator>();
    }

    private void Update() {
        var currentInput = Input.GetAxis("Horizontal");
        var absCurrentInput = Mathf.Abs(currentInput);

        if (absCurrentInput > .1f) {
            var currentPosition = this.transform.position;
            currentPosition.x += this.movementSpeed * Time.deltaTime * currentInput;
            this.transform.position = currentPosition;

            var currentScale = this.transform.localScale;
            currentScale.x = -Mathf.Sign(currentInput);
            this.transform.localScale = currentScale;
        }

        if (this.animator) {
            this.animator.SetFloat("movementSpeed", absCurrentInput);
        }
    }
}
