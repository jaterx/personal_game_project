using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideRhino : MonoBehaviour {

	public static GameObject fpsGun;
	public static GameObject fpsChar;
	public static GameObject buildMenu;
	public static GameObject driveTip;
	public float distance;
	public static Vector3 oldPos;
	public static Text driveTxt;
	public static bool rhinoDrive;
	public static GameObject[] rhinoStore;
	public static GameObject rhinoO;
	public static bool drivePass;
	// Use this for initialization
	void Start () {
		fpsGun = GameObject.FindGameObjectWithTag ("fpsGun");
		fpsChar = GameObject.FindGameObjectWithTag ("Player");
		buildMenu = GameObject.FindGameObjectWithTag ("BuildMenu");
		rhinoDrive = false;
		drivePass = false;
	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		rhinoStore = GameObject.FindGameObjectsWithTag ("Rhino");

		if (MapMagicDemo.CameraController.controlMode == 0) {
			foreach (GameObject rhinoS in rhinoStore) {
				distance = Vector3.Distance (rhinoS.transform.position, fpsGun.transform.position);
				if (distance <= 8.0f) {
					rhinoO = rhinoS;
					if (Input.GetKey (KeyCode.F)) {
						rhinoDrive = true;
					}
				}
			}
		}

		if (rhinoDrive == true){
			fpsGun.SetActive (false);
			fpsChar.SetActive (false);
			buildMenu.SetActive (false);
			//isoSwitch.accessCamF.GetComponent<Camera>().transform.LookAt (this.gameObject.transform.position);
			drivePass = true;
			MapMagicDemo.CameraController.driveMode = 1;
			//quit drive
			if (Input.GetKey (KeyCode.G)) {
				rhinoDrive = false;
				fpsGun.SetActive (true);
				buildMenu.SetActive (true);
				fpsChar.SetActive (true);
				drivePass = false;
				MapMagicDemo.CameraController.driveMode = 0;
			}

		}

	}
}
