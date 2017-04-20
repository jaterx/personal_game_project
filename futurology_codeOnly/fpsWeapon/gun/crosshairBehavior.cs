using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshairBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		this.transform.Rotate (0, 0, 1);
		if (Input.GetMouseButton(0)){
			transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
			if (transform.localScale.magnitude > 0.6f){
				transform.localScale = new Vector3 (0.6f,0.6f,0.6f);
			}
		}
		if (!Input.GetMouseButton(0)){
			transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
			if(transform.localScale.magnitude > 0.4f){
				transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
			}
		}




	}
}
