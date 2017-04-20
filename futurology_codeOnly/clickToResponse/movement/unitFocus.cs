using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;

public class unitFocus : MonoBehaviour {

	public static Camera isoCam;
	public static GameObject camRef;
	RaycastHit hit;
	public Text selectionText;
	public string selectionText2;
	public string savedName;
	public string savedDynamicName;
	public static bool focusMode;
	public static bool unitTracking;
	public GameObject savedObject;
	public static GameObject savedObjectForUI;
	public static GameObject savedEnemyObjectForUI;
	public static GameObject savedObjectForHP;
	public Text camTrackingText;
	public string camTrackingText2;
	public static GameObject indicator;
	public static GameObject indicator1;
	public static GameObject indicator2;
	public static GameObject hpf;
	public static GameObject hpn;
	public static Text objInfo;
	public static bool uiTracker;
	public int referenceHP;
	public int damagedHP;
	public static bool dExit;
	public AudioSource[] unitSFX;

	public static bool retreat;

	//public GameObject hpf;
	public GameObject hpfu;
	public GameObject hpnu;
	public GameObject hpnr;
	public GameObject hpfr;
	public static float damageProvidingUAV;
	public static float damageProvidingRhino;
	public static RectTransform hpfuRT;
	public static RectTransform hpfrRT;
	public static bool hpBarCaptured;
	public static bool hpBarCapturedR;

	Rigidbody rbForHP;

	// Use this for initialization
	void Start () {
		isoCam = isoSwitchBack.isoCamera.GetComponent<Camera>();
		camRef = isoSwitchBack.camRef;
		//originalCamY = camRef.transform.position.y;
		focusMode = false;
		unitTracking = false;
		selectionText = GameObject.FindGameObjectWithTag("Selection").GetComponent<Text>();
		camTrackingText = GameObject.FindGameObjectWithTag("CamTracking").GetComponent<Text>();
		indicator1 = GameObject.FindGameObjectWithTag ("Indicator");
		indicator2 = GameObject.FindGameObjectWithTag ("CircleIndicator");
		objInfo = GameObject.FindGameObjectWithTag ("InfoPanelObj").GetComponent<Text>();
		hpf = GameObject.FindGameObjectWithTag ("HPF");
		hpn = GameObject.FindGameObjectWithTag ("HPN");
		uiTracker = false;
		referenceHP = (int)hpn.GetComponent<RectTransform> ().rect.width;
		damagedHP = (int)hpf.GetComponent<RectTransform> ().rect.width;
		dExit = false;
		unitSFX = this.gameObject.GetComponents<AudioSource> ();

		//hpfu = GameObject.FindGameObjectWithTag ("HPFU");
		hpnu = GameObject.FindGameObjectWithTag ("HPNU");
		hpnr = GameObject.FindGameObjectWithTag ("HPNR");
		hpf = GameObject.FindGameObjectWithTag ("HPF");
		hpf.GetComponent<RectTransform>().sizeDelta = new Vector2 (0, 5);
		hpnu.transform.position = new Vector3 (99999,99999,99999);
		hpnr.transform.position = new Vector3 (99999,99999,99999);
		//hpfu.transform.position = hpnu.transform.position;
		damageProvidingUAV = 100.0f;

		hpBarCaptured = false;
		hpBarCapturedR = false;
		retreat = false;

	}

	// Update is called once per frame
	void Update () {
		
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		Ray ray = isoCam.ScreenPointToRay (Input.mousePosition);
		ray.origin = camRef.transform.position;

		selectionText.text = selectionText2;
		camTrackingText.text = camTrackingText2;

		if (Input.GetKey (KeyCode.Alpha1) && MapMagicDemo.CameraController.controlMode == 1) {
			focusMode = false;
		}
		if (Input.GetKey(KeyCode.Alpha2) && MapMagicDemo.CameraController.controlMode == 1){
			focusMode = true;
		}

		if(focusMode == false){
			camTrackingText2 = "2: Focus Lock\nFree";
		}
		if(focusMode == true){
			camTrackingText2 = "1: Focus Lock\nOn Unit";
		}

		if (dExit == true){
			dExit = false;
		}

		if (Input.GetKey(KeyCode.G) && rideUAV.uavDrive == true && MapMagicDemo.CameraController.controlMode == 1){
			dExit = true;
			selectionText2 = "Drive mode exited\nYou can now press del\nto sell this unit";
		}
		if (Input.GetKey(KeyCode.G) && RideRhino.rhinoDrive == true && MapMagicDemo.CameraController.controlMode == 1){
			dExit = true;
			selectionText2 = "Drive mode exited\nYou can now press del\nto sell this unit";
		}

		//get focus, update UI and camera position
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray, out hit, 10000)) {

				//non-automatic camera track (traditional rts games)
				if (hit.transform.name == "Observatory Base" || hit.transform.name == "Generator" || hit.transform.name == "Satellite") {
					//Debug.Log (hit.transform.name);
					selectionText2 = "Press del to sell this unit\nRight click to cancel selection\nObservatory can not be deleted";
					if (focusMode == true) {
						camRef.transform.position = new Vector3 ((hit.transform.position.x - 70.0f), camRef.transform.position.y, (hit.transform.position.z - 75.0f));
					}
					savedObjectForUI = hit.collider.gameObject;
					savedName = hit.transform.name;
					uiTracker = true;

					if (hit.transform.name == "Observatory Base"){
						unitSFX[1].Play();
					}
					if (hit.transform.name == "Satellite"){
						unitSFX[0].Play();
					}
					if (hit.transform.name == "Generator"){
						unitSFX[2].Play();
					}

				}

				if (hit.transform.name == "UAV" || hit.transform.name == "Rhino") {
					clickToMove.flag = 1;
					clickToFly.flag = 1;
					savedDynamicName = hit.transform.name;
					if (clickToMove.flag == 1 || clickToFly.flag == 1) {
						selectionText2 = "Press del to sell this unit\nLeft Click to select target location\nRight click to cancel selection";
					}
					if (focusMode == true){
						savedObject = hit.collider.gameObject;
						unitTracking = true;
					}
					savedObjectForUI = hit.collider.gameObject;
					uiTracker = true;

				}

				if (hit.transform.name == "BaseE") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "GeneratorE") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "GeneratorE2") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "SatelliteE") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "RhinoE") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "RhinoE2") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "RhinoE3") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "RhinoE4") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "UAVE") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "UAVE2") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "UAVE3") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
				if (hit.transform.name == "UAVE4") {
					savedEnemyObjectForUI = hit.collider.gameObject;
				}
			}
		}

		//lost focus, update UI only
		if (savedName == "Observatory Base" || savedName == "Generator" || savedName == "Satellite") {
			if (/*Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D) ||*/ Input.GetMouseButtonDown (1)) {
				selectionText2 = " ";
				savedName = " ";
			}

		}
		if (savedDynamicName == "Rhino"){
			if (unitTracking == true) {
				camRef.transform.position = new Vector3 ((savedObject.transform.position.x - 70.0f), camRef.transform.position.y, (savedObject.transform.position.z - 75.0f));
			}
			if (clickToMove.flag == 2 || Input.GetMouseButtonDown (1)) {
				selectionText2 = " ";
				savedDynamicName = " ";
				unitTracking = false;
			}
			hpBarCapturedR = true;
		}

		if (savedDynamicName == "UAV"){
			if (unitTracking == true) {
				camRef.transform.position = new Vector3 ((savedObject.transform.position.x - 70.0f), camRef.transform.position.y, (savedObject.transform.position.z - 75.0f));
			}
			if (clickToFly.flag == 2 || Input.GetMouseButtonDown (1)) {
				selectionText2 = " ";
				savedDynamicName = " ";
				unitTracking = false;
			}
			//uav damaged;
			hpBarCaptured = true;
		}

		if (uiTracker == true){
			hpn.transform.position = isoCam.WorldToScreenPoint (new Vector3(savedObjectForUI.transform.position.x, savedObjectForUI.transform.position.y + 10.0f, savedObjectForUI.transform.position.z));
			hpf.transform.position = hpn.transform.position;
			if (savedObjectForUI.name == "UAV"){
				objInfo.text = "UAV Trident\n" + "Flying Unit\n" + "HP " + (int)(savedObjectForUI.GetComponent<Rigidbody>().mass-400) + "%";
				if (Input.GetKeyUp(KeyCode.Delete)) {
					if (rideUAV.drivePass == false) {
						Destroy (savedObjectForUI.gameObject);
						hpn.transform.position = new Vector3 (99999, 99999, 99999);
						hpf.transform.position = hpn.transform.position;
						objInfo.text = "Friendly Unit\nNothing selected";
						uiTracker = false;
						selectionText2 = "Unit Sold\nCurrency Refunded\nright click to continue";
						dExit = false;
						GlobalControl.unitCount -= 1;
						GlobalControl.uavCount -= 1;
						GlobalControl.currency += 20000;
					}
					if (rideUAV.drivePass == true) {
						if (dExit == false) {
							selectionText2 = "You must exit drive mode first\nin order to sell this unit\npress G now to exit drive mode";
						}
					}
				}

				if (hpBarCaptured == true) {
					hpfu = savedObjectForUI.transform.Find ("HPBar/HPFU").gameObject;
					if(MapMagicDemo.CameraController.controlMode == 1){
						hpnu.transform.position = hpn.transform.position;
						hpfu.transform.position = hpnu.transform.position;
					}
					hpfuRT = hpfu.GetComponent<RectTransform> ();
					//damageProvidingUAV = ;
					hpfuRT.sizeDelta = new Vector2 (savedObjectForUI.GetComponent<Rigidbody> ().mass - 400.0f, 5);
					savedObjectForHP = hpfu;
					//Debug.Log ("HPN Pos: " + hpn.transform.position);
					//Debug.Log ("HPFU Pos: " + hpnu.transform.position);
					//Debug.Log ("Damage Providing: " + damageProvidingUAV);
				}
			}

			if (savedObjectForUI.name == "Rhino"){
				objInfo.text = "Spectra Rhino\n" + "Ground Unit\n" + "HP " + (int)(savedObjectForUI.GetComponent<Rigidbody>().mass-400) + "%";
				if (Input.GetKeyUp(KeyCode.Delete)){
					if (RideRhino.drivePass == false) {
						Destroy (savedObjectForUI.gameObject);
						hpn.transform.position = new Vector3 (99999, 99999, 99999);
						hpf.transform.position = hpn.transform.position;
						objInfo.text = "Friendly Unit\nNothing selected";
						uiTracker = false;
						selectionText2 = "Unit Sold\nCurrency Refunded\nright click to continue";
						dExit = false;
						GlobalControl.unitCount -= 1;
						GlobalControl.rhinoCount -= 1;
						GlobalControl.currency += 10000;
					}if (RideRhino.drivePass == true) {
						if (dExit == false) {
							selectionText2 = "You must exit drive mode first\nin order to sell this unit\npress G now to exit drive mode";
						}
					}
				}

				if (hpBarCapturedR == true) {
					hpfr = savedObjectForUI.transform.Find ("HPBar/HPFR").gameObject;
					if(MapMagicDemo.CameraController.controlMode == 1){
						hpnr.transform.position = hpn.transform.position;
						hpfr.transform.position = hpnr.transform.position;
					}
					hpfrRT = hpfr.GetComponent<RectTransform> ();
					//damageProvidingUAV = ;
					hpfrRT.sizeDelta = new Vector2 (savedObjectForUI.GetComponent<Rigidbody> ().mass - 400.0f, 5);
					savedObjectForHP = hpfr;
					//Debug.Log ("HPN Pos: " + hpn.transform.position);
					//Debug.Log ("HPFU Pos: " + hpnu.transform.position);
					//Debug.Log ("Damage Providing: " + damageProvidingUAV);
				}
			}
			if (savedObjectForUI.name == "Satellite"){
				objInfo.text = "Satellite\n" + "Building\n" + "HP " + (int)(savedObjectForUI.GetComponent<Rigidbody>().mass-400) + "%";
				if (Input.GetKeyUp(KeyCode.Delete)){
					Destroy(savedObjectForUI.gameObject);
					hpn.transform.position =  new Vector3(99999,99999,99999);
					hpf.transform.position = hpn.transform.position;
					objInfo.text = "Friendly Unit\nNothing selected";
					uiTracker = false;
					selectionText2 = "Unit Sold\nCurrency Refunded\nright click to continue";
					GlobalControl.unitCount -= 1;
					GlobalControl.sateCount -= 1;
					GlobalControl.currency += 8000;
				}
			}
			if (savedObjectForUI.name == "Generator"){
				objInfo.text = "Generator\n" + "Building\n" + "HP " + (int)(savedObjectForUI.GetComponent<Rigidbody>().mass-400) + "%";
				if (Input.GetKeyUp(KeyCode.Delete)){
					Destroy(savedObjectForUI.gameObject);
					hpn.transform.position =  new Vector3(99999,99999,99999);
					hpf.transform.position = hpn.transform.position;
					objInfo.text = "Friendly Unit\nNothing selected";
					uiTracker = false;
					selectionText2 = "Unit Sold\nCurrency Refunded\nright click to continue";
					GlobalControl.unitCount -= 1;
					GlobalControl.genCount -= 1;
					GlobalControl.currency += 7000;
				}
			}
			if (savedObjectForUI.name == "Observatory Base"){
				objInfo.text = "Observatory\n" + "Building\n" + "HP " + (int)(savedObjectForUI.GetComponent<Rigidbody>().mass-400) + "%";
			}

		}
		if(uiTracker == false){
			hpn.transform.position =  new Vector3(99999,99999,99999);
			hpnu.transform.position =  new Vector3(99999,99999,99999);
			hpnr.transform.position =  new Vector3(99999,99999,99999);
			hpf.transform.position = hpn.transform.position;
			objInfo.text = "Friendly Unit\nNothing selected";
			hpBarCaptured = false;
			hpBarCapturedR = false;
			if (savedObjectForHP != null && hpBarCaptured == false){
				savedObjectForHP.transform.position = new Vector3(99999,99999,99999);
			}
		}

		//calculate current player's rhino
		foreach (GameObject friendlyRhino in GameObject.FindGameObjectsWithTag ("Rhino")) {
			float distanceRhinoR1 = Vector3.Distance (friendlyRhino.transform.position, NavRhinoE.getThis.transform.position);
			if (distanceRhinoR1 <= NavRhinoE.attackingRange && GlobalControl.r1Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceRhinoR2 = Vector3.Distance (friendlyRhino.transform.position, NavRhinoE2.getThis.transform.position);
			if (distanceRhinoR2 <= NavRhinoE2.attackingRange && GlobalControl.r2Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceRhinoR3 = Vector3.Distance (friendlyRhino.transform.position, NavRhinoE3.getThis.transform.position);
			if (distanceRhinoR3 <= NavRhinoE3.attackingRange && GlobalControl.r3Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceRhinoR4 = Vector3.Distance (friendlyRhino.transform.position, NavRhinoE4.getThis.transform.position);
			if (distanceRhinoR4 <= NavRhinoE4.attackingRange && GlobalControl.r4Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceRhinoU1 = Vector3.Distance (friendlyRhino.transform.position, NavEU.getThis.transform.position);
			if (distanceRhinoU1 <= NavEU.attackingRange && GlobalControl.u1Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceRhinoU2 = Vector3.Distance (friendlyRhino.transform.position, NavEU2.getThis.transform.position);
			if (distanceRhinoU2 <= NavEU2.attackingRange && GlobalControl.u2Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceUAVU3 = Vector3.Distance (friendlyRhino.transform.position, NavEU3.getThis.transform.position);
			if (distanceUAVU3 <= NavEU3.attackingRange && GlobalControl.u3Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceRhinoU4 = Vector3.Distance (friendlyRhino.transform.position, NavEU4.getThis.transform.position);
			if (distanceRhinoU4 <= NavEU4.attackingRange && GlobalControl.u4Killed == false) {
				friendlyRhino.GetComponent<Rigidbody> ().mass -= 0.1f;
			}

			if (friendlyRhino.GetComponent<Rigidbody> ().mass <= 400){
				friendlyRhino.GetComponent<Rigidbody> ().mass = 400;
			}
			if (friendlyRhino.transform.Find ("HPBar/HPFR").gameObject.GetComponent<RectTransform>().sizeDelta.x <= 0){
				RideRhino.rhinoDrive = false;
				RideRhino.drivePass = false;
				friendlyRhino.transform.position = new Vector3 (99999,99999,99999);
				//NavEU.distanceRhino = 88888;
				Destroy (friendlyRhino);
				hpn.transform.position =  new Vector3(99999,99999,99999);
				hpnr.transform.position =  new Vector3(99999,99999,99999);
				hpf.transform.position = hpn.transform.position;
				objInfo.text = "Friendly Unit\nNothing selected";
				uiTracker = false;
				GlobalControl.unitCount -= 1;
				GlobalControl.rhinoCount -= 1;
			}
		}

		//calculate player's base
		foreach(GameObject friendlyBase in GameObject.FindGameObjectsWithTag("Base")){
			float distanceBaseR1 = Vector3.Distance (friendlyBase.transform.position, NavRhinoE.getThis.transform.position);
			if (distanceBaseR1 <= NavRhinoE.attackingRange && GlobalControl.r1Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceBaseR2 = Vector3.Distance (friendlyBase.transform.position, NavRhinoE2.getThis.transform.position);
			if (distanceBaseR2 <= NavRhinoE2.attackingRange && GlobalControl.r2Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceBaseR3 = Vector3.Distance (friendlyBase.transform.position, NavRhinoE3.getThis.transform.position);
			if (distanceBaseR3 <= NavRhinoE3.attackingRange && GlobalControl.r3Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceBaseR4 = Vector3.Distance (friendlyBase.transform.position, NavRhinoE4.getThis.transform.position);
			if (distanceBaseR4 <= NavRhinoE4.attackingRange && GlobalControl.r4Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceBaseU1 = Vector3.Distance (friendlyBase.transform.position, NavEU.getThis.transform.position);
			if (distanceBaseU1 <= NavEU.attackingRange && GlobalControl.u1Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceBaseU2 = Vector3.Distance (friendlyBase.transform.position, NavEU2.getThis.transform.position);
			if (distanceBaseU2 <= NavEU2.attackingRange && GlobalControl.u2Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceBaseU3 = Vector3.Distance (friendlyBase.transform.position, NavEU3.getThis.transform.position);
			if (distanceBaseU3 <= NavEU3.attackingRange && GlobalControl.u3Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceBaseU4 = Vector3.Distance (friendlyBase.transform.position, NavEU4.getThis.transform.position);
			if (distanceBaseU4 <= NavEU4.attackingRange && GlobalControl.u4Killed == false) {
				friendlyBase.GetComponent<Rigidbody> ().mass -= 0.1f;
			}

			if (friendlyBase.GetComponent<Rigidbody> ().mass <= 400){
				friendlyBase.GetComponent<Rigidbody> ().mass = 400;
			}
			if (friendlyBase.GetComponent<Rigidbody>().mass <= 400){
				friendlyBase.transform.position = new Vector3 (99999,99999,99999);
				Destroy (friendlyBase);
				objInfo.text = "Friendly Unit\nNothing selected";
				uiTracker = false;
				GlobalControl.unitCount -= 1;
				GlobalControl.baseCount -= 1;
			}
		}

		//calculate player's satellite
		foreach(GameObject friendlySatellite in GameObject.FindGameObjectsWithTag("Dish")){
			float distanceSatelliteR1 = Vector3.Distance (friendlySatellite.transform.position, NavRhinoE.getThis.transform.position);
			if (distanceSatelliteR1 <= NavRhinoE.attackingRange && GlobalControl.r1Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceSatelliteR2 = Vector3.Distance (friendlySatellite.transform.position, NavRhinoE2.getThis.transform.position);
			if (distanceSatelliteR2 <= NavRhinoE2.attackingRange && GlobalControl.r2Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceSatelliteR3 = Vector3.Distance (friendlySatellite.transform.position, NavRhinoE3.getThis.transform.position);
			if (distanceSatelliteR3 <= NavRhinoE3.attackingRange && GlobalControl.r3Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceGeneratorR4 = Vector3.Distance (friendlySatellite.transform.position, NavRhinoE4.getThis.transform.position);
			if (distanceGeneratorR4 <= NavRhinoE4.attackingRange && GlobalControl.r4Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceSatelliteU1 = Vector3.Distance (friendlySatellite.transform.position, NavEU.getThis.transform.position);
			if (distanceSatelliteU1 <= NavEU.attackingRange && GlobalControl.u1Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceSatelliteU2 = Vector3.Distance (friendlySatellite.transform.position, NavEU2.getThis.transform.position);
			if (distanceSatelliteU2 <= NavEU2.attackingRange && GlobalControl.u2Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceSatelliteU3 = Vector3.Distance (friendlySatellite.transform.position, NavEU3.getThis.transform.position);
			if (distanceSatelliteU3 <= NavEU3.attackingRange && GlobalControl.u3Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceSatelliteU4 = Vector3.Distance (friendlySatellite.transform.position, NavEU4.getThis.transform.position);
			if (distanceSatelliteU4 <= NavEU4.attackingRange && GlobalControl.u4Killed == false) {
				friendlySatellite.GetComponent<Rigidbody> ().mass -= 0.1f;
			}

			if (friendlySatellite.GetComponent<Rigidbody> ().mass <= 400){
				friendlySatellite.GetComponent<Rigidbody> ().mass = 400;
			}
			if (friendlySatellite.GetComponent<Rigidbody>().mass <= 400){
				friendlySatellite.transform.position = new Vector3 (99999,99999,99999);
				Destroy (friendlySatellite);
				objInfo.text = "Friendly Unit\nNothing selected";
				uiTracker = false;
				GlobalControl.unitCount -= 1;
				GlobalControl.baseCount -= 1;
			}
		}

		//calculate player's generator
		foreach(GameObject friendlyGenerator in GameObject.FindGameObjectsWithTag("Generator")){
			float distanceGeneratorR1 = Vector3.Distance (friendlyGenerator.transform.position, NavRhinoE.getThis.transform.position);
			if (distanceGeneratorR1 <= NavRhinoE.attackingRange && GlobalControl.r1Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceGeneratorR2 = Vector3.Distance (friendlyGenerator.transform.position, NavRhinoE2.getThis.transform.position);
			if (distanceGeneratorR2 <= NavRhinoE2.attackingRange && GlobalControl.r2Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceGeneratorR3 = Vector3.Distance (friendlyGenerator.transform.position, NavRhinoE3.getThis.transform.position);
			if (distanceGeneratorR3 <= NavRhinoE3.attackingRange && GlobalControl.r3Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceGeneratorR4 = Vector3.Distance (friendlyGenerator.transform.position, NavRhinoE4.getThis.transform.position);
			if (distanceGeneratorR4 <= NavRhinoE4.attackingRange && GlobalControl.r4Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceGeneratorU1 = Vector3.Distance (friendlyGenerator.transform.position, NavEU.getThis.transform.position);
			if (distanceGeneratorU1 <= NavEU.attackingRange && GlobalControl.u1Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceGeneratorU2 = Vector3.Distance (friendlyGenerator.transform.position, NavEU2.getThis.transform.position);
			if (distanceGeneratorU2 <= NavEU2.attackingRange && GlobalControl.u2Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceGeneratorU3 = Vector3.Distance (friendlyGenerator.transform.position, NavEU3.getThis.transform.position);
			if (distanceGeneratorU3 <= NavEU3.attackingRange && GlobalControl.u3Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceGeneratorU4 = Vector3.Distance (friendlyGenerator.transform.position, NavEU4.getThis.transform.position);
			if (distanceGeneratorU4 <= NavEU4.attackingRange && GlobalControl.u4Killed == false) {
				friendlyGenerator.GetComponent<Rigidbody> ().mass -= 0.1f;
			}

			if (friendlyGenerator.GetComponent<Rigidbody> ().mass <= 400){
				friendlyGenerator.GetComponent<Rigidbody> ().mass = 400;
			}
			if (friendlyGenerator.GetComponent<Rigidbody>().mass <= 400){
				friendlyGenerator.transform.position = new Vector3 (99999,99999,99999);
				Destroy (friendlyGenerator);
				objInfo.text = "Friendly Unit\nNothing selected";
				uiTracker = false;
				GlobalControl.unitCount -= 1;
				GlobalControl.baseCount -= 1;
			}
		}

		//calculate current player's uav
		foreach (GameObject friendlyUAV in GameObject.FindGameObjectsWithTag ("UAV")) {
			float distanceUAVR1 = Vector3.Distance (friendlyUAV.transform.position, NavRhinoE.getThis.transform.position);
			if (distanceUAVR1 <= NavRhinoE.attackingRange && GlobalControl.r1Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceUAVR2 = Vector3.Distance (friendlyUAV.transform.position, NavRhinoE2.getThis.transform.position);
			if (distanceUAVR2 <= NavRhinoE2.attackingRange && GlobalControl.r2Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceUAVR3 = Vector3.Distance (friendlyUAV.transform.position, NavRhinoE3.getThis.transform.position);
			if (distanceUAVR3 <= NavRhinoE3.attackingRange && GlobalControl.r3Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceUAVR4 = Vector3.Distance (friendlyUAV.transform.position, NavRhinoE4.getThis.transform.position);
			if (distanceUAVR4 <= NavRhinoE4.attackingRange && GlobalControl.r4Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.05f;
			}
			float distanceUAVU1 = Vector3.Distance (friendlyUAV.transform.position, NavEU.getThis.transform.position);
			if (distanceUAVU1 <= NavEU.attackingRange && GlobalControl.u1Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceUAVU2 = Vector3.Distance (friendlyUAV.transform.position, NavEU2.getThis.transform.position);
			if (distanceUAVU2 <= NavEU2.attackingRange && GlobalControl.u2Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceUAVU3 = Vector3.Distance (friendlyUAV.transform.position, NavEU3.getThis.transform.position);
			if (distanceUAVU3 <= NavEU3.attackingRange && GlobalControl.u3Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.1f;
			}
			float distanceUAVU4 = Vector3.Distance (friendlyUAV.transform.position, NavEU4.getThis.transform.position);
			if (distanceUAVU4 <= NavEU4.attackingRange && GlobalControl.u4Killed == false) {
				friendlyUAV.GetComponent<Rigidbody> ().mass -= 0.1f;
			}

			if (friendlyUAV.GetComponent<Rigidbody> ().mass <= 400){
				friendlyUAV.GetComponent<Rigidbody> ().mass = 400;
			}
			if (friendlyUAV.transform.Find ("HPBar/HPFU").gameObject.GetComponent<RectTransform>().sizeDelta.x <= 0){
				rideUAV.uavDrive = false;
				rideUAV.drivePass = false;
				friendlyUAV.transform.position = new Vector3 (99999,99999,99999);
				//NavEU.distanceUAV = 88888;
				Destroy (friendlyUAV);
				hpn.transform.position =  new Vector3(99999,99999,99999);
				hpnu.transform.position =  new Vector3(99999,99999,99999);
				hpf.transform.position = hpn.transform.position;
				objInfo.text = "Friendly Unit\nNothing selected";
				uiTracker = false;
				GlobalControl.unitCount -= 1;
				GlobalControl.uavCount -= 1;
				UAVattack.uavShot = false;
			}
		}
	}
}
