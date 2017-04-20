using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStormRenderer : MonoBehaviour {

	public GameObject charchar;
	public GameObject refref;
	// Use this for initialization
	void Start () {
		charchar = GameObject.FindGameObjectWithTag ("Char");
		refref = GameObject.FindGameObjectWithTag ("camRef");
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		if (MapMagicDemo.CameraController.controlMode == 0){
			this.transform.position = charchar.transform.position;
		}
		if (MapMagicDemo.CameraController.controlMode == 1){
			this.transform.position = new Vector3(refref.transform.position.x, this.transform.position.y, refref.transform.position.z+30.0f);
		}
	}
}
