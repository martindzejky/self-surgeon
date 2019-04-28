using UnityEngine;

public class Knife : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Enemy") {
            var bloodCell = collider.gameObject.GetComponent<BloodCell>();
            if (bloodCell) {
                bloodCell.Die();
            }
        }
    }
}
