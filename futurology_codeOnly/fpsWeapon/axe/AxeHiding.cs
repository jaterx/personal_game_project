using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHiding : MonoBehaviour {

	public GameObject axe;

	// Use this for initialization
	void Start () {
		axe = GameObject.FindGameObjectWithTag ("Axe");
	}
	
	// Update is called once per frame
	void Update () {
		if (MapMagicDemo.CameraController.controlMode == 0){
			if (Input.GetKeyUp(KeyCode.I)){
				axe.SetActive (true);
			}
			if (Input.GetKeyUp(KeyCode.K)){
				axe.SetActive (false);
			}
		}
	}
}
