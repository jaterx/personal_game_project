using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzleBehavior : MonoBehaviour {

	public GameObject gun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		transform.position = gunBehavior.originalPos;
		if (gunBehavior.shot == true) {
			GetComponent<Light> ().enabled = true;
		}
		if (gunBehavior.shot == false) {
			GetComponent<Light> ().enabled = false;
		}
		//Debug.Log (gunBehavior.shot);
	}
}
