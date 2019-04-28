using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateAction : MonoBehaviour {
    public void StartOperation() {
        GlobalGameController.globalInstance.StartOperation();
    }
}
