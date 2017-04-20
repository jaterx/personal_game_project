using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVAnimator : MonoBehaviour {

	Animator anim;
	public static float stunt;
	public static float rightM;
	public static float leftM;
	public static bool stuntConfirmed;
	public static Vector3 storedRotation;
	//public Quaternion startRotation;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		anim.enabled = false;
		//startRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		/*
		if (MapMagicDemo.CameraController.controlMode == 0) {
			if (rideUAV.uavDrive == true){
				if (Input.GetKeyUp(KeyCode.R)){
					//anim.enabled = true;
					stunt = 3.0f;
				}
				if (!Input.GetKeyUp(KeyCode.R)){
					//anim.enabled = false;
					stunt = 1.0f;
					if (Input.GetKey(KeyCode.A)) {
						leftM = 3.0f;
						rightM = 1.0f;
					}
					if (Input.GetKey(KeyCode.D)) {
						leftM = 1.0f;
						rightM = 3.0f;
					}
					if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
						leftM = 1.0f;
						rightM = 1.0f;
					}
				}

			}
			anim.SetFloat ("Stunt", stunt);
			anim.SetFloat ("Left", leftM);
			anim.SetFloat ("Right", rightM);
			if (Input.GetKey(KeyCode.Alpha1)){
				anim.enabled = true;
			}
			if (Input.GetKey(KeyCode.Alpha2)){
				anim.enabled = false;
			}
		}*/
		if (MapMagicDemo.CameraController.controlMode == 1) {
			anim.enabled = true;
			if (unitFocus.savedObjectForUI == this.gameObject){

				if (Input.GetKeyUp (KeyCode.R)) {
					//anim.enabled = true;
					stunt = 3.0f;
				}
				if (!Input.GetKeyUp (KeyCode.R)) {
					//anim.enabled = false;
					stunt = 1.0f;
				}
			}
			anim.SetFloat ("Stunt", stunt);
		}
	}

}
