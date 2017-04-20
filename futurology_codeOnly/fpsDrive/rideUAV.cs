using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rideUAV : MonoBehaviour {

	public static GameObject fpsGun;
	public static GameObject buildMenu;
	public static GameObject driveTip;
	public float distance;
	public static Vector3 oldPos;
	public static Text driveTxt;
	public static bool uavDrive;
	public static GameObject[] uavStore;
	public static GameObject uavO;
	public static bool drivePass;
	public static GameObject fpsChar;
	// Use this for initialization
	void Start () {
		fpsGun = GameObject.FindGameObjectWithTag ("fpsGun");
		fpsChar = GameObject.FindGameObjectWithTag ("Player");
		buildMenu = GameObject.FindGameObjectWithTag ("BuildMenu");
		uavDrive = false;
		drivePass = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		uavStore = GameObject.FindGameObjectsWithTag ("UAV");

		if (MapMagicDemo.CameraController.controlMode == 0) {
			foreach (GameObject uavS in uavStore) {
				distance = Vector3.Distance (uavS.transform.position, fpsGun.transform.position);
				if (distance <= 12.0f) {
					uavO = uavS;
					if (Input.GetKey (KeyCode.F)) {
						uavDrive = true;
					}
				}
			}
		}

		if (uavDrive == true){
			fpsGun.SetActive (false);
			fpsChar.SetActive (false);
			buildMenu.SetActive (false);
			MapMagicDemo.CameraController.accessChar.GetComponent<MapMagicDemo.CharController> ().gravity = false;
			//isoSwitch.accessCamF.GetComponent<Camera>().transform.LookAt (this.gameObject.transform.position);
			drivePass = true;
			MapMagicDemo.CameraController.driveMode = 1;
			//quit drive
			if (Input.GetKey (KeyCode.G)) {
				uavDrive = false;
				fpsGun.SetActive (true);
				buildMenu.SetActive (true);
				fpsChar.SetActive (true);
				drivePass = false;
				MapMagicDemo.CameraController.accessChar.GetComponent<MapMagicDemo.CharController> ().gravity = true;
				MapMagicDemo.CameraController.driveMode = 0;
			}

		}

	}
}
