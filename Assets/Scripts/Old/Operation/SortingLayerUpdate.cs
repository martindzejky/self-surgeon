using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortingLayerUpdate : MonoBehaviour {
    private SpriteRenderer sp;

    public void Awake() {
        this.sp = this.GetComponent<SpriteRenderer>();
    }

    public void Update() {
        this.sp.sortingOrder = Convert.ToInt32(-this.transform.position.y * 1000);
    }
}
