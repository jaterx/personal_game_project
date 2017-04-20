using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour {

	public static int qualityLvl;
	public GameObject low;
	public GameObject med;
	public GameObject high;
	public GameObject ultra;
	public GameObject pcmr;
	public GameObject optionPanel;
	public GameObject mMenu;
	public GameObject fName;
	public GameObject tName;
	public GameObject lTxt;
	public GameObject rTxt;
	public GameObject mainCam;
	public Component bloom;
	public Component fxaa;
	public Component lensFlare;
	public Component sunShafts;
	public static bool oKeyDown;

	// Use this for initialization
	void Start () {
		low = GameObject.Find ("LowLevel");
		med = GameObject.Find ("MedLevel");
		high = GameObject.Find ("HighLevel");
		ultra = GameObject.Find ("UltraLevel");
		pcmr = GameObject.Find ("PCMRLevel");
		optionPanel = GameObject.Find ("OptionPanel");
		mMenu = GameObject.Find ("MainMenu");
		tName = GameObject.Find ("tName");
		fName = GameObject.Find ("fName");
		lTxt = GameObject.Find ("lText");
		rTxt = GameObject.Find ("rText");
		mainCam = isoSwitch.accessCMR;
		optionPanel.SetActive (false);
		qualityLvl = 3;
		oKeyDown = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (Input.GetKeyUp(KeyCode.O)){
			oKeyDown = true;
			mMenu.SetActive (false);
			tName.SetActive (false);
			fName.SetActive (false);
			lTxt.SetActive (false);
			rTxt.SetActive (false);
		}
		if (Input.GetKeyUp(KeyCode.P)){
			oKeyDown = false;
			mMenu.SetActive (true);
			tName.SetActive (true);
			fName.SetActive (true);
			lTxt.SetActive (true);
			rTxt.SetActive (true);
		}
		//Debug.Log (oKeyDown);

		if (oKeyDown == false){
			optionPanel.SetActive (false);
		}

		if (oKeyDown == true){
			optionPanel.SetActive (true);

			if (qualityLvl <= 5){
				if (Input.GetKeyUp(KeyCode.RightArrow)){
					qualityLvl += 1;
				}
			}
			if (qualityLvl >= 0){
				if (Input.GetKeyUp(KeyCode.LeftArrow)){
					qualityLvl -= 1;
				}
			}

			if (qualityLvl == 0){
				low.SetActive (true);
				med.SetActive (false);
				high.SetActive (false);
				ultra.SetActive (false);
				pcmr.SetActive (false);
				if (Input.GetKey(KeyCode.Return)){
					QualitySettings.SetQualityLevel(0, true);
					GlobalControl.qualityCounter = 0;
					//mainCam.GetComponent<BloomOptimized> ().enabled = false;
					//mainCam.GetComponent<Antialiasing> ().enabled = false;
					//mainCam.GetComponent<LensFlare> ().enabled = false;
					//mainCam.GetComponent<SunShafts> ().enabled = false;
				}
			}
			if (qualityLvl == 1){
				low.SetActive (false);
				med.SetActive (true);
				high.SetActive (false);
				ultra.SetActive (false);
				pcmr.SetActive (false);
				if (Input.GetKey(KeyCode.Return)){
					QualitySettings.SetQualityLevel(1, true);
					GlobalControl.qualityCounter = 1;
					//mainCam.GetComponent<BloomOptimized> ().enabled = false;
					//mainCam.GetComponent<Antialiasing> ().enabled = false;
					//mainCam.GetComponent<LensFlare> ().enabled = false;
					//mainCam.GetComponent<SunShafts> ().enabled = true;
				}
			}
			if (qualityLvl == 2){
				low.SetActive (false);
				med.SetActive (false);
				high.SetActive (true);
				ultra.SetActive (false);
				pcmr.SetActive (false);
				if (Input.GetKey(KeyCode.Return)){
					QualitySettings.SetQualityLevel(2, true);
					GlobalControl.qualityCounter = 2;
					//mainCam.GetComponent<BloomOptimized> ().enabled = true;
					//mainCam.GetComponent<Antialiasing> ().enabled = false;
					//mainCam.GetComponent<LensFlare> ().enabled = true;
					//mainCam.GetComponent<SunShafts> ().enabled = true;
				}
			}
			if (qualityLvl == 3){
				low.SetActive (false);
				med.SetActive (false);
				high.SetActive (false);
				ultra.SetActive (true);
				pcmr.SetActive (false);
				if (Input.GetKey(KeyCode.Return)){
					QualitySettings.SetQualityLevel(3, true);
					GlobalControl.qualityCounter = 3;
					//mainCam.GetComponent<BloomOptimized> ().enabled = true;
					//mainCam.GetComponent<Antialiasing> ().enabled = true;
					//mainCam.GetComponent<LensFlare> ().enabled = true;
					//mainCam.GetComponent<SunShafts> ().enabled = true;
				}
			}
			if (qualityLvl == 4){
				low.SetActive (false);
				med.SetActive (false);
				high.SetActive (false);
				ultra.SetActive (false);
				pcmr.SetActive (true);
				if (Input.GetKey(KeyCode.Return)){
					QualitySettings.SetQualityLevel(4, true);
					GlobalControl.qualityCounter = 4;
					//mainCam.GetComponent<BloomOptimized> ().enabled = true;
					//mainCam.GetComponent<Antialiasing> ().enabled = true;
					//mainCam.GetComponent<LensFlare> ().enabled = true;
					//mainCam.GetComponent<SunShafts> ().enabled = true;
				}
			}
		}

	}
}
