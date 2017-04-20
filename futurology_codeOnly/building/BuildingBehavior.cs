using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingBehavior : MonoBehaviour {

	//public GameObject terrains;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		//terrains = GameObject.FindGameObjectWithTag ("Terrain");
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
	}

	void OnTriggerEnter(Collider other) {
		
	}
}
