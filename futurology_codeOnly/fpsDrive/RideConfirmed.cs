using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideConfirmed : MonoBehaviour {

	public float distance2;
	public static Text driveTxt;
	public static GameObject driveTip;
	public static GameObject fpsGun2;

	// Use this for initialization
	void Start () {
		driveTip = GameObject.FindGameObjectWithTag ("driveTip");
		driveTxt = driveTip.GetComponent<Text> ();
		fpsGun2 = GameObject.FindGameObjectWithTag ("fpsGun");
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		distance2 = Vector3.Distance (this.transform.position, fpsGun2.transform.position);
		if (distance2 <= 12.0f){
			driveTxt.text = "F: Drive UAV";
		}
		if (distance2 > 12.0f && rideUAV.uavDrive == false) {
			driveTxt.text = " ";
		}
		if (rideUAV.uavDrive == true) {
			driveTxt.text = "G: Exit UAV";
		}
			
		if (rideUAV.uavDrive == false){
			Vector3 pos = transform.position;
			pos.y = Terrain.activeTerrain.SampleHeight (transform.position);
			transform.position = pos;
			carPhysics.carSpeed = 0;
		}

	}
}
