using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingEffect : MonoBehaviour {

	public float decay;
	public float intensity;
	public Vector3 originalPos;
	public Quaternion originalRot;

	// Use this for initialization
	void Start () {
		intensity = 0.02f;
		decay = 0.002f;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (MapMagicDemo.CameraController.controlMode == 0) {
			originalPos = this.transform.position;
			originalRot = this.transform.rotation;
			if (carPhysics.shiftPressed == true) {
				if (intensity > 0) {
					transform.position = originalPos + Random.insideUnitSphere * intensity;
					transform.rotation = new Quaternion (originalRot.x + Random.Range (-intensity, intensity) * 0.2f, originalRot.y + Random.Range (-intensity, intensity) * 0.2f, originalRot.z + Random.Range (-intensity, intensity) * 0.2f, originalRot.w + Random.Range (-intensity, intensity) * 0.2f);
					intensity -= decay;
				}
				if(intensity <= 0){
					intensity = 0.02f;
				}
			}
		}
	}
}
