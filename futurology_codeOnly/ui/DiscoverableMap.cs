using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverableMap : MonoBehaviour {

	public float thisX;
	public float thisY;
	public float thisSpeed;
	public Vector3 originalPos;
	public Vector3 originalScale;
	public Vector3 screenPos;
	public Camera isoCamRef;
	public GameObject isoChar;

	// Use this for initialization
	void Start () {
		thisX = 1.76f / 10;
		thisY = 1.76f / 10;
		originalPos = this.transform.position;
		originalScale = this.transform.localScale;
		isoCamRef = GameObject.FindGameObjectWithTag ("IsoCamera").GetComponent<Camera>();
		isoChar = GameObject.FindGameObjectWithTag ("Char");
	}
	
	// Update is called once per frame
	void Update () {

		//thisX = thisX * 1.76f;
		//thisY = thisY * 0.88f;

		//952,237

		//Debug.Log(isoCamRef.WorldToScreenPoint(isoChar.transform.position));

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (Input.GetKey(KeyCode.W)){ //move shade forward
				this.transform.position += this.transform.up * thisY;
				this.transform.position += this.transform.right * thisX;
			}
			if (Input.GetKey(KeyCode.S)){ //move shade backward
				this.transform.position -= this.transform.up * thisY;
				this.transform.position -= this.transform.right * thisX;
			}
			if (Input.GetKey(KeyCode.A)){ //move shade left
				this.transform.position -= this.transform.right * thisX;
				this.transform.position += this.transform.up * thisY;
			}
			if (Input.GetKey(KeyCode.D)){ //move shade right
				this.transform.position += this.transform.right * thisX;
				this.transform.position -= this.transform.up * thisY;
			}

			if (isoSwitchBack.camRef.transform.position.y <= 125){
				if (Input.GetAxis ("Mouse ScrollWheel") < 0) { //out
					this.transform.localScale *= 1.01f;
				}
			}

			if (isoSwitchBack.camRef.transform.position.y >= 30) {
				if (Input.GetAxis ("Mouse ScrollWheel") > 0) { //in
					this.transform.localScale /= 1.01f;
				}
			}

			//Debug.Log (isoCamRef.WorldToScreenPoint (isoChar.transform.position).x / (-4.984f));

			//Debug.Log (isoCamRef.WorldToScreenPoint (isoChar.transform.position).y / (-1.364f));
			//reset
			if (isoSwitchBack.camRef.transform.position.y == 88.0f) {
				this.transform.localScale = originalScale;
			}

			if (Input.GetKey (KeyCode.V)) {
				//this.transform.position = originalPos;
			}
			if (Input.GetKey (KeyCode.C)) {
				//this.transform.position = originalPos;
			}
		}
	}
}
