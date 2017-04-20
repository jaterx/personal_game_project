using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhinoConfirmed : MonoBehaviour {

	public float distance2;
	public static Text driveTxt;
	public static GameObject driveTip;
	public static GameObject fpsGun2;
	RaycastHit hits;
	Vector3 currentNormal;
	Quaternion initialRotation;
	Quaternion smoothRotation;

	// Use this for initialization
	void Start () {
		driveTip = GameObject.FindGameObjectWithTag ("driveTip");
		driveTxt = driveTip.GetComponent<Text> ();
		fpsGun2 = GameObject.FindGameObjectWithTag ("fpsGun");
		initialRotation = transform.rotation;
		//this.gameObject.transform.Rotate (0,90,0);
	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		distance2 = Vector3.Distance (this.transform.position, fpsGun2.transform.position);
		if (distance2 <= 8.0f){
			driveTxt.text = "F: Ride Rhino";
		}
		if (distance2 > 8.0f) {
			driveTxt.text = " ";
		}

		if (RideRhino.rhinoDrive == true){
			driveTxt.text = "G: Exit Rhino";
		}

		if (RideRhino.rhinoDrive == false) {
			Vector3 pos = transform.position;
			pos.y = Terrain.activeTerrain.SampleHeight (transform.position) - 10.0f;
			transform.position = pos;
			RhinoPhysics.carSpeed = 0;
		}

		//align ground movable object with surface normal, currentNormal is denoted for smooth alignment
		if (RideRhino.rhinoDrive == true) {
			if (Physics.Raycast (transform.position, -Vector3.up, out hits, 10)) {
				currentNormal = Vector3.Lerp (currentNormal, hits.normal, 2*Time.deltaTime);
				smoothRotation = Quaternion.FromToRotation (Vector3.up, currentNormal);
				transform.rotation = smoothRotation * initialRotation * RhinoPhysics.outRotation;
			}
		}
	}
}
