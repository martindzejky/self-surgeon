using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateAction : MonoBehaviour {
    public AudioClip sound;

    public void StartOperation() {
        AudioPlayer.Play(this.sound);
        GlobalGameController.globalInstance.StartOperation();
    }
}
