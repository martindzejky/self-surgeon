using UnityEngine;

public class LargeHumanMovement : MonoBehaviour {
    public float movementSpeed;

    private void Update() {
        var currentInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(currentInput) > .1f) {
            var currentPosition = this.transform.position;
            currentPosition.x += this.movementSpeed * Time.deltaTime * currentInput;
            this.transform.position = currentPosition;

            var currentScale = this.transform.localScale;
            currentScale.x = -Mathf.Sign(currentInput);
            this.transform.localScale = currentScale;
        }
    }
}
