using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManipulation : MonoBehaviour {
    public AudioClip pickupSound;
    public AudioClip putdownSound;

    public bool lifted = false;

    private Camera mainCamera;
    private Player player;
    private SpriteRenderer spriteRenderer;

    public void Awake() {
        this.mainCamera = Camera.main;
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void Update() {
        this.HandlePicking();

        if (this.lifted) {
            this.spriteRenderer.sortingLayerName = "Tools";
            this.MoveToPlayer();
            this.RotateTowardsMouse();
        } else {
            this.spriteRenderer.sortingLayerName = "Floor";
        }
    }

    private void HandlePicking() {
        if (!this.player) {
            this.lifted = false;
            return;
        }

        var distanceToPlayer = (
            this.player.transform.position -
            this.transform.position
        ).magnitude;

        if (distanceToPlayer < 1.3f && Input.GetButtonDown("Secondary")) {
            if (this.lifted) {
                this.lifted = false;
                this.player.carriedTool = null;
                Debug.Log("Tool put down " + this.gameObject.name);
                AudioPlayer.PlayAtPositionWithPitch(this.transform.position, this.putdownSound, 1, .1f);
            } else if (!player.carriedTool) {
                this.lifted = true;
                this.player.carriedTool = this.gameObject;
                Debug.Log("Tool lifted " + this.gameObject.name);
                AudioPlayer.PlayAtPositionWithPitch(this.transform.position, this.pickupSound, 1, .1f);
            }
        }
    }

    private void MoveToPlayer() {
        this.transform.position = new Vector3(
            this.player.transform.position.x,
            this.player.transform.position.y + .44f,
            this.transform.position.z
        );
    }
    
    private void RotateTowardsMouse() {
        var mousePosition = this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var directionTowardsMouse = (mousePosition - this.transform.position).normalized;

        transform.rotation = Quaternion.Euler(
            0,
            0,
            Mathf.Atan2(directionTowardsMouse.y, directionTowardsMouse.x) * Mathf.Rad2Deg
        );
    }
}
