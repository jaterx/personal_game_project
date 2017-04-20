using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnselControllerCamera : MonoBehaviour {

	public GameObject fpsUI;
	public GameObject fpsGun;
	public GameObject cControl;
	public GameObject pControl;
	public Vector3 uiPos;

	// Use this for initialization
	void Start () {
		fpsUI = GameObject.FindGameObjectWithTag ("FpsUI");
		fpsGun = GameObject.FindGameObjectWithTag ("fpsGun");
		cControl = GameObject.FindGameObjectWithTag ("CurrencyF");
		pControl = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true){
			fpsUI.gameObject.SetActive (false);
			fpsGun.gameObject.SetActive (false);
			cControl.SetActive (false);
			pControl.SetActive (false);
			return;
		}
		if (NVIDIA.Ansel.IsSessionActive == false && MapMagicDemo.CameraController.controlMode == 0 && rideUAV.uavDrive == false && RideRhino.rhinoDrive == false) {
			fpsUI.gameObject.SetActive (true);
			fpsGun.gameObject.SetActive (true);
			cControl.SetActive (true);
			pControl.SetActive (true);
		}

	}
}
