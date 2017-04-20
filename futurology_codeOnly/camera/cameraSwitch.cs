using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour {

	public GameObject cameraAnother;
	public GameObject charAnother;
	public static GameObject isoCamera;
	public static GameObject fpsCamera;
	public static GameObject isoUIM;
	public static GameObject isoUII;
	public static bool keyPass;
	public Vector3 recordedPos;
	public Vector3 recordedPos2;
	public static float speed;

	// Use this for initialization
	void Start () {
		cameraAnother = GameObject.FindGameObjectWithTag ("Char");
		//isoCamera = GameObject.FindGameObjectWithTag ("IsoCamera");
		isoUII = GameObject.Find("InfoPanel2");
		isoUIM = GameObject.Find("MiniMap2");
		recordedPos = isoUII.transform.position;
		recordedPos2 = isoUIM.transform.position;
		isoUII.transform.position = new Vector3 (99999,99999,99999);
		isoUIM.transform.position = new Vector3 (99999,99999,99999);
		cameraAnother.SetActive(false);
		speed = 1.0f;
		//cameraAnother2.SetActive(false);
		//cisoCamera.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		//this.transform.position = charAnother.transform.position;
		if (Input.GetKey (KeyCode.Space)) {
			GameObject.FindGameObjectWithTag ("MenuChar").SetActive(false);
			cameraAnother.SetActive(true);
			//isoCamera.SetActive(false);
			cameraAnother.transform.position = this.transform.position;
			isoUII.transform.position = recordedPos;
			isoUIM.transform.position = recordedPos2;

		}
	}
}
