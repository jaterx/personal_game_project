using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {
	public GameObject gun;
	public Vector3 originalPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		if (MapMagicDemo.CameraController.controlMode == 0) {
			transform.position += transform.forward * Time.deltaTime * 100f;
			if ((transform.forward - gunBehavior.originalPos).magnitude <= 30) {
				
			}
		}
	}
}
