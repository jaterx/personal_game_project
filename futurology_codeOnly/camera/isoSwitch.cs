using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class isoSwitch : MonoBehaviour {

	public static GameObject isoCamera;
	public static GameObject accessIso;
	public static GameObject accessCMR;
	public static GameObject fpsUI;
	public static GameObject isoUI;
	public MonoBehaviour CameraController;
	public static Vector3 recordedPos;
	public static GameObject wT;
	public static GameObject wF;
	public static GameObject bg;
	public static bool leftScroll;
	public static bool rightScroll;
	public static float speed;
	public Vector3 leftRec;
	public Vector3 rightRec;
	public Vector3 farLeftRec;
	public Component menuBGM;
	public AudioSource[] fpsSFX;
	public static GameObject accessCamF;
	public static bool firstSwitch;
	//public static int bgNumber;

	// Use this for initialization
	void Start () {
		isoCamera = GameObject.Find ("IsoCamera");
		accessIso = isoCamera;
		accessCMR = GameObject.Find ("camReference");
		fpsUI = GameObject.Find("FpsUI");
		isoUI = GameObject.Find("IsoUI");
		bg = GameObject.Find ("bg");
		wT = GameObject.Find ("TundraWallPaper");
		wF = GameObject.Find ("SavannahWallPaper");
		recordedPos = fpsUI.transform.position;
		speed = 10.0f;
		//isoCamera.SetActive (false);
		leftRec = wT.transform.position;
		rightRec = wF.transform.position;
		farLeftRec = new Vector3 ((wT.transform.position.x - 1920.0f), wT.transform.position.y, wT.transform.position.z);
		//menuBGM = this.gameObject.GetComponent<AudioSource> ();
		fpsSFX = this.gameObject.GetComponents<AudioSource> ();
		accessCamF = this.gameObject;
		firstSwitch = true;
		//bgNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//isoCamera.transform.position = this.transform.position;
		//isoCamera.transform.position += transform.up * 85.0f;
		//main menu actions

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if(Input.GetKey(KeyCode.C)){
			bg.SetActive (false);
			fpsSFX [0].volume = 0.0f;
			fpsSFX [1].volume = 0.5f;
			fpsSFX [2].volume = 0.5f;
			fpsSFX [3].volume = 0.5f;
		}

		if (Input.GetKey (KeyCode.V)) {
			if (firstSwitch == true) {
				GameObject.FindGameObjectWithTag ("camRef").transform.position = new Vector3 (MapMagicDemo.CameraController.accessChar.transform.position.x - 35.0f, 88.0f, MapMagicDemo.CameraController.accessChar.transform.position.z - 35.0f);
				firstSwitch = false;
			}
			Cursor.visible = true;
			isoCamera.SetActive (true);
			fpsUI.transform.position = recordedPos;
			fpsUI.SetActive (false);
			isoUI.SetActive (true);
			isoSwitchBack.tundraBGM.UnPause();
			isoSwitchBack.tundraSFX1.UnPause();
			isoSwitchBack.tundraSFX2.UnPause();
			isoSwitchBack.scroller.transform.position = isoSwitchBack.scrollerRef.transform.position;
			bg.SetActive (false);
			fpsSFX [0].volume = 0.0f;
			fpsSFX [1].volume = 0.0f;
			fpsSFX [2].volume = 0.0f;
			fpsSFX [3].volume = 0.0f;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			wF.SetActive (true);
			wT.SetActive (false);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			wT.SetActive (true);
			wF.SetActive (false);
		}

		//In Game Option Menu

		//Alogrithm 1
		//Disbaled due to performance issue
		/*
		if (Input.GetKeyUp(KeyCode.Backspace)){
			bgNumber += 1;
		}
		if(bgNumber % 2 != 0){
			bg.SetActive (true);
		}
		if(bgNumber % 2 == 0){
			bg.SetActive (false);
		}*/

		//Alogrithm 2

		if (GlobalControl.qualityCounter == 0){
			this.GetComponent<BloomOptimized> ().enabled = false;
			this.GetComponent<Antialiasing> ().enabled = false;
			this.GetComponent<BloomAndFlares> ().enabled = false;
			this.GetComponent<SunShafts> ().enabled = false;
		}
		if (GlobalControl.qualityCounter == 1){
			this.GetComponent<BloomOptimized> ().enabled = false;
			this.GetComponent<Antialiasing> ().enabled = false;
			this.GetComponent<BloomAndFlares> ().enabled = false;
			this.GetComponent<SunShafts> ().enabled = true;
		}
		if (GlobalControl.qualityCounter == 2){
			this.GetComponent<BloomOptimized> ().enabled = true;
			this.GetComponent<Antialiasing> ().enabled = false;
			this.GetComponent<BloomAndFlares> ().enabled = true;
			this.GetComponent<SunShafts> ().enabled = false;
		}
		if (GlobalControl.qualityCounter == 3){
			this.GetComponent<BloomOptimized> ().enabled = true;
			this.GetComponent<Antialiasing> ().enabled = true;
			this.GetComponent<BloomAndFlares> ().enabled = true;
			this.GetComponent<SunShafts> ().enabled = true;
		}
		if (GlobalControl.qualityCounter == 4){
			this.GetComponent<BloomOptimized> ().enabled = true;
			this.GetComponent<Antialiasing> ().enabled = true;
			this.GetComponent<BloomAndFlares> ().enabled = true;
			this.GetComponent<SunShafts> ().enabled = true;
		}

	}
}
