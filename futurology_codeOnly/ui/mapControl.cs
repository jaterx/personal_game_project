using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapControl : MonoBehaviour {

	public GameObject charRo;

	public float rX;
	public float rZ;

	public float rotationX = 0;
	public float rotationY = 190;

	// Use this for initialization
	void Start () {
		charRo = GameObject.FindGameObjectWithTag ("MainCamera");

	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		rotationY += Input.GetAxis("Mouse X");
		rotationX -= Input.GetAxis("Mouse Y");

		this.transform.localEulerAngles = new Vector3(0, 0, rotationY);
		//Debug.Log (charRo.transform.rotation.y);
	}
}
