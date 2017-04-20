using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isoSwitchBack : MonoBehaviour {

	public static GameObject fpsCamera;
	public static GameObject isoCamera;
	public static GameObject camRef;
	public static GameObject bgmMixer;
	public static GameObject elevationMixer;
	public static GameObject sfxMixer;
	public static GameObject scroller;
	public static GameObject scrollerRef;
	public float speed;
	public static bool elevationReset;
	public static AudioSource[] audioContainer2;
	public static AudioSource tundraBGM;
	public static AudioSource tundraSFX1;
	public static AudioSource tundraSFX2;
	public static AudioSource tundraSFX3;
	public static Text bgmTxt;
	public static Text sfxTxt;
	public static Text elevationTxt;
	public float elevationY;
	public float sfxVol;
	public float bgmVol;
	//public GameObject fpsUI;

	// Use this for initialization
	void Start () {
		isoCamera = this.gameObject;
		fpsCamera = GameObject.FindGameObjectWithTag ("Char");
		camRef = GameObject.FindGameObjectWithTag ("camRef");
		bgmMixer = GameObject.FindGameObjectWithTag ("bgmm");
		sfxMixer = GameObject.FindGameObjectWithTag ("sfxx");
		elevationMixer = GameObject.FindGameObjectWithTag ("elvv");
		scroller = GameObject.FindGameObjectWithTag ("handle");
		scrollerRef = GameObject.FindGameObjectWithTag ("handle2");
		audioContainer2 = this.gameObject.GetComponents<AudioSource> ();
		speed = 1.0f;
		elevationReset = false;
		bgmTxt = bgmMixer.GetComponent<Text>();
		sfxTxt = sfxMixer.GetComponent<Text>();
		elevationTxt = elevationMixer.GetComponent<Text>();
		tundraBGM = audioContainer2 [0];
		tundraSFX1 = audioContainer2 [1];
		tundraSFX2 = audioContainer2 [2];
		tundraSFX3 = audioContainer2 [3];
		tundraBGM.Pause();
		tundraSFX1.Pause();
		tundraSFX2.Pause();
		tundraSFX3.Pause();
		tundraBGM.volume = 0f;
		tundraSFX1.volume = 0.5f;
		tundraSFX2.volume = 0.5f;
		tundraSFX3.volume = 0.5f;
		elevationY = 88.0f;
		scroller.transform.position = scrollerRef.transform.position;
		//mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		//this.gameObject.SetActive(false);
		//fpsCamera.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		//Debug.Log (Cursor.lockState);

		this.transform.position = camRef.transform.position;

		if (Input.GetKey (KeyCode.C)) {
			//fpsCamera.SetActive (true);
			//mainCamera.SetActive (true);
			this.gameObject.SetActive (false);
			isoSwitch.fpsUI.SetActive (true);
			tundraBGM.Pause();
			tundraSFX1.Pause ();
			tundraSFX2.Pause ();
			//camRef.transform.position = new Vector3 (MapMagicDemo.CameraController.accessChar.transform.position.x - 35.0f, 88.0f, MapMagicDemo.CameraController.accessChar.transform.position.z - 35.0f);
			tundraBGM.volume = 0f;
			tundraSFX1.volume = 0.5f;
			tundraSFX2.volume = 0.5f;
			//isoSwitch.fpsUI.transform.position = recordedFpsUIPos;
		}

		//camera movement under isometric view
		if (Input.GetKey(KeyCode.W)){ //move camera forward
			camRef.transform.position += camRef.transform.forward * speed;
		}
		if (Input.GetKey(KeyCode.S)){ //move camera backward
			camRef.transform.position -= camRef.transform.forward * speed;
		}
		if (Input.GetKey(KeyCode.A)){ //move camera left
			camRef.transform.position -= camRef.transform.right * speed;
		}
		if (Input.GetKey(KeyCode.D)){ //move camera right
			camRef.transform.position += camRef.transform.right * speed;
		}
		if (camRef.transform.position.y <= 125) {
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) { //zooming out
				camRef.transform.position -= camRef.transform.up * Input.GetAxis ("Mouse ScrollWheel") * 10;
				if (camRef.transform.position.y >= 88.0f) {
					tundraBGM.volume += 0.03f;
				}
				tundraSFX1.volume -= 0.015f;
				tundraSFX2.volume -= 0.015f;
				tundraSFX3.volume -= 0.015f;
				scroller.transform.position -= scroller.transform.up * Input.GetAxis ("Mouse ScrollWheel") * 38.0f;
			}
		}
		if (camRef.transform.position.y >= 30) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) { //zooming in
				camRef.transform.position -= camRef.transform.up * Input.GetAxis ("Mouse ScrollWheel") * 10;
				tundraBGM.volume -= 0.03f;
				tundraSFX1.volume += 0.015f;
				tundraSFX2.volume += 0.015f;
				tundraSFX3.volume += 0.015f;
				scroller.transform.position -= scroller.transform.up * Input.GetAxis ("Mouse ScrollWheel") * 38.0f;
			}
		}

		//reset isometric's camera's elevation by a single mouse wheel click when over zoomed
		if (Input.GetMouseButtonDown(2)){
			elevationReset = true;
		}
		if (elevationReset == true) {
			if (camRef.transform.position.y < 88.0f) {
				camRef.transform.position += camRef.transform.up * speed;
				scroller.transform.position += scroller.transform.up * 3.8f;

			}
			if (camRef.transform.position.y > 88.0f) {
				camRef.transform.position -= camRef.transform.up * speed;
				scroller.transform.position -= scroller.transform.up * 3.8f;

			}
			if (camRef.transform.position.y == 88.0f) {
				elevationReset = false;
				tundraBGM.volume = 0;
				tundraSFX1.volume = 0.5f;
				tundraSFX2.volume = 0.5f;
				tundraSFX3.volume = 0.5f;
				scroller.transform.position = scrollerRef.transform.position;
			}
		}

		bgmVol = tundraBGM.volume * 100;
		sfxVol = tundraSFX1.volume * 100;
		elevationY = camRef.transform.position.y;
		bgmTxt.text = "BGM "+(int)bgmVol+"%";
		sfxTxt.text = "\nSFX "+(int)sfxVol+"%";
		elevationTxt.text = "\n\nElevation "+(int)elevationY+"M";
	}
}
