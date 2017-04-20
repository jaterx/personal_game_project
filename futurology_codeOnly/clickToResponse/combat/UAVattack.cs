using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVattack : MonoBehaviour {

	public static bool uavShot;
	public static GameObject uavBullet;
	public static GameObject uav;
	public static GameObject currentUAV;
	public static Vector3 savedPosL;
	public static Vector3 savedPosR;
	public float uavAttackDistance;
	public static bool manuallyAttack;

	// Use this for initialization
	void Start () {
		uavShot = false;
		uavBullet = GameObject.FindWithTag("UAVBullet");
		uavAttackDistance = 30.0f;
		manuallyAttack = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		//uav shot under isometric view
		if (MapMagicDemo.CameraController.controlMode == 1) {
			if (clickToFly.objectSelected == true && unitFocus.uiTracker == true) {
				if (clickToFly.savedObject.name == "UAV") {
					if (Input.GetKey (KeyCode.Z)) {
						manuallyAttack = true;
						uavShot = true;
					}
					if (Input.GetKey (KeyCode.X)) {
						manuallyAttack = false;
						uavShot = false;
					}
				}
			}
			if (uavShot == true) {
				GameObject bulletShot = GameObject.Instantiate (uavBullet, clickToFly.savedObject.transform.position, clickToFly.savedObject.transform.rotation) as GameObject;
				bulletShot.transform.Translate (6.0f, -0.5f, -1.0f);
				savedPosL = bulletShot.transform.position;
				GameObject bulletShot2 = GameObject.Instantiate (uavBullet, clickToFly.savedObject.transform.position, clickToFly.savedObject.transform.rotation) as GameObject;
				bulletShot2.transform.Translate (-6.0f, -0.5f, -1.0f);
				savedPosR = bulletShot2.transform.position;
				GameObject.Destroy (bulletShot, 1f);
				GameObject.Destroy (bulletShot2, 1f);
			}
		}

		//uav shot under first person view
		if (MapMagicDemo.CameraController.controlMode == 0) {
			if (rideUAV.drivePass == true){
				if (Input.GetMouseButton(0)) {
					manuallyAttack = true;
					uavShot = true;
				}
				if (uavShot == true) {
					currentUAV = rideUAV.uavO;
					GameObject bulletShot = GameObject.Instantiate (uavBullet, currentUAV.transform.position, currentUAV.transform.rotation) as GameObject;
					bulletShot.transform.Translate (6.0f, -0.5f, -1.0f);
					savedPosL = bulletShot.transform.position;
					GameObject bulletShot2 = GameObject.Instantiate (uavBullet, currentUAV.transform.position, currentUAV.transform.rotation) as GameObject;
					bulletShot2.transform.Translate (-6.0f, -0.5f, -1.0f);
					savedPosR = bulletShot2.transform.position;
					GameObject.Destroy (bulletShot, 3f);
					GameObject.Destroy (bulletShot2, 3f);
				}
				if (!Input.GetMouseButton(0)) {
					manuallyAttack = false;
					uavShot = false;
				}
			}
		}

		//semi-automatic uav shot
		float distanceUR1 = Vector3.Distance (this.transform.position, NavRhinoE.getThis.transform.position);
		float distanceUR2 = Vector3.Distance (this.transform.position, NavRhinoE2.getThis.transform.position);
		float distanceUR3 = Vector3.Distance (this.transform.position, NavRhinoE3.getThis.transform.position);
		float distanceUR4 = Vector3.Distance (this.transform.position, NavRhinoE4.getThis.transform.position);
		float distanceUU1 = Vector3.Distance (this.transform.position, NavEU.getThis.transform.position);
		float distanceUU2 = Vector3.Distance (this.transform.position, NavEU2.getThis.transform.position);
		float distanceUU3 = Vector3.Distance (this.transform.position, NavEU3.getThis.transform.position);
		float distanceUU4 = Vector3.Distance (this.transform.position, NavEU4.getThis.transform.position);
		if (distanceUR1 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavRhinoE.getThis.transform);
		}
		if (distanceUR2 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavRhinoE2.getThis.transform);
		}
		if (distanceUR3 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavRhinoE3.getThis.transform);
		}
		if (distanceUR4 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavRhinoE4.getThis.transform);
		}
		if (distanceUU1 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavEU.getThis.transform);
		}
		if (distanceUU2 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavEU2.getThis.transform);
		}
		if (distanceUU3 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavEU3.getThis.transform);
		}
		if (distanceUU4 <= uavAttackDistance){
			uavShot = true;
			this.transform.LookAt (NavEU4.getThis.transform);
		}
		if (distanceUR1 > uavAttackDistance && distanceUR2 > uavAttackDistance && distanceUR3 > uavAttackDistance && distanceUR4 > uavAttackDistance && distanceUU1 > uavAttackDistance && distanceUU2 > uavAttackDistance && distanceUU3 > uavAttackDistance && distanceUU4 > uavAttackDistance){
			if (manuallyAttack == false) {
				uavShot = false;
			}
		}
	}
}
