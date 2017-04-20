using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVAllAttack : MonoBehaviour {

	public static bool allAttack;

	// Use this for initialization
	void Start () {
		allAttack = false;	
	}

	// Update is called once per frame
	void Update () {
		if (MapMagicDemo.CameraController.controlMode == 1) {
			//if (unitFocus.savedObjectForUI == this.gameObject) {
				if (Input.GetKey (KeyCode.Q)) {
					allAttack = true;
				}
				if (allAttack == true) {
					GameObject bulletShot = GameObject.Instantiate (UAVattack.uavBullet, this.transform.position, this.transform.rotation) as GameObject;
					bulletShot.transform.Translate (6.0f, -0.5f, -1.0f);
					//UAVattack.savedPosL = bulletShot.transform.position;
					GameObject bulletShot2 = GameObject.Instantiate (UAVattack.uavBullet, this.transform.position, this.transform.rotation) as GameObject;
					bulletShot2.transform.Translate (-6.0f, -0.5f, -1.0f);
					//UAVattack.savedPosR = bulletShot2.transform.position;
					GameObject.Destroy (bulletShot, 1f);
					GameObject.Destroy (bulletShot2, 1f);
					if (Input.GetKey (KeyCode.E)) {
						allAttack = false;
					}
				}
			//}
		}
	}
}
