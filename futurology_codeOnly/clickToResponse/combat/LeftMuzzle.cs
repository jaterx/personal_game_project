using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMuzzle : MonoBehaviour {

	public GameObject gun;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (UAVattack.uavShot == true) {
			this.transform.position = UAVattack.savedPosL;
			GetComponent<Light> ().enabled = true;
		}
		if (UAVattack.uavShot == false) {
			GetComponent<Light> ().enabled = false;
		}
		//Debug.Log (gunBehavior.shot);
	}
}