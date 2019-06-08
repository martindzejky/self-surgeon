using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraActivator : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        GetComponent<CinemachineVirtualCamera>().enabled = true;
        Debug.Log("Entered " + gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D other) {
        GetComponent<CinemachineVirtualCamera>().enabled = false;
        Debug.Log("Left " + gameObject.name);
    }
}
