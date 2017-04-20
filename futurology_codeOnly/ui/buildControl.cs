using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildControl : MonoBehaviour {
	public GameObject baseui;
	public GameObject genui;
	public GameObject uavui;
	public GameObject rhinoui;
	public GameObject sateui;
	public GameObject subui;

	public static GameObject baseStation;
	public static GameObject uav;
	public static GameObject satellite;
	public static GameObject rhino;
	public static GameObject generator;

	public GameObject baseBuilt;

	public GameObject charInfo;
	public Camera camInfo;

	public GameObject tip;
	public GameObject warning;
	public Text warningTxt;

	public static int menuNum;

	public static bool baseHold;
	public static bool genHold;
	public static bool sateHold;
	public static bool uavHold;
	public static bool rhinoHold;

	public static bool buildAvailable;

	public static bool lockCursor = true;

	public static int rNumber;
	public static int uNumber;

	public MapMagicDemo.CameraController cc;

	RaycastHit hits;
	Vector3 currentNormalB;
	Quaternion initialRotationB;
	Quaternion smoothRotationB;
	Vector3 currentNormalG;
	Quaternion initialRotationG;
	Quaternion smoothRotationG;
	Vector3 currentNormalS;
	Quaternion initialRotationS;
	Quaternion smoothRotationS;

	public static bool baseAlreadyBuilt;
	public static bool genAlreadyBuilt;
	public static bool sateAlreadyBuilt;
	public static bool uavAlreadyBuilt;
	public static bool rhinoAlreadyBuilt;

	public static bool gameStarted;

	// Use this for initialization
	void Start () {

		charInfo = GameObject.FindGameObjectWithTag ("Char");

		baseui = GameObject.FindGameObjectWithTag ("BaseUI");
		genui = GameObject.FindGameObjectWithTag ("GeneratorUI");
		sateui = GameObject.FindGameObjectWithTag ("SatelliteUI");
		rhinoui = GameObject.FindGameObjectWithTag ("RhinoUI");
		uavui = GameObject.FindGameObjectWithTag ("DroneUI");
		subui = GameObject.FindGameObjectWithTag ("BuildWareUI");

		baseStation = GameObject.FindGameObjectWithTag ("Base");
		generator = GameObject.FindGameObjectWithTag ("Generator");
		satellite = GameObject.FindGameObjectWithTag ("Dish");
		rhino = GameObject.FindGameObjectWithTag ("Rhino");
		uav = GameObject.FindGameObjectWithTag ("UAV");

		tip = GameObject.FindGameObjectWithTag ("Tip");
		warning = GameObject.FindGameObjectWithTag ("Warning");
		warningTxt = warning.GetComponent <Text>();
		//baseBuilt = GameObject.FindGameObjectWithTag ("BaseBuilt");

		menuNum = 0;

		baseHold = false;
		genHold = false;
		sateHold = false;
		rhinoHold = false;
		uavHold = false;

		baseui.SetActive(false);
		genui.SetActive(false);
		uavui.SetActive(false);
		rhinoui.SetActive(false);
		sateui.SetActive(false);
		subui.SetActive(false);
		tip.SetActive (false);

		rNumber = 0;
		uNumber = 0;

		buildAvailable = true;

		baseAlreadyBuilt = false;
		genAlreadyBuilt = false;
		sateAlreadyBuilt = false;
		uavAlreadyBuilt = false;
		rhinoAlreadyBuilt = false;

		gameStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		//Debug.Log (menuNum);

		if (Input.GetKey(KeyCode.LeftAlt)){
			warningTxt.text = " ";
		}

		if (baseHold == true) {
			baseStation.SetActive (true);
			baseStation.transform.position = charInfo.transform.position;
			baseStation.GetComponent<Rigidbody> ().isKinematic = true;
			baseStation.transform.Translate(0,0,-30);

			Vector3 pos2 = baseStation.transform.position;
			pos2.y = Terrain.activeTerrain.SampleHeight (baseStation.transform.position);
			baseStation.transform.position = pos2;

			tip.SetActive (true);
			if (Input.GetKeyDown (KeyCode.LeftAlt)) {
				baseStation.SetActive (false);
				menuNum = 0;
				tip.SetActive (false);
				baseHold = false;
			}
			if (Input.GetMouseButtonUp (1)) {
				if (GlobalControl.baseCount == 0) {
					baseHold = false;
					GameObject baseBuilt = GameObject.Instantiate (baseStation, baseStation.transform.position, baseStation.transform.rotation) as GameObject;
					baseBuilt.name = "Observatory Base";

					Vector3 pos = baseBuilt.transform.position;
					pos.y = Terrain.activeTerrain.SampleHeight (baseBuilt.transform.position);
					baseBuilt.transform.position = pos;

					if (Physics.Raycast (baseBuilt.transform.position, -Vector3.up, out hits, 1000)) {
						currentNormalB = Vector3.Lerp (currentNormalB, hits.normal, 4 * Time.deltaTime);
						baseBuilt.transform.up = hits.normal;
					}

					baseBuilt.GetComponent<Rigidbody> ().isKinematic = false;
					baseStation.SetActive (false);
					menuNum = 0;
					tip.SetActive (false);
					GlobalControl.baseCount += 1;
					GlobalControl.currency -= 5000;

					baseAlreadyBuilt = true;
					gameStarted = true;
				}
			}
			if (GlobalControl.baseCount == 1){
				if (Input.GetMouseButton (1)) {
					warningTxt.text = "There can only exist\none base at the same time";
				}
			}
		}

		if (genHold == true) {
			generator.SetActive (true);
			generator.transform.position = charInfo.transform.position;
			generator.GetComponent<Rigidbody> ().isKinematic = true;
			generator.transform.Translate(0,0,-30);
			tip.SetActive (true);

			Vector3 pos2 = generator.transform.position;
			pos2.y = Terrain.activeTerrain.SampleHeight (generator.transform.position)-12.0f;
			generator.transform.position = pos2;

			if (Input.GetKeyDown (KeyCode.LeftAlt)) {
				generator.SetActive (false);
				menuNum = 0;
				tip.SetActive (false);
				genHold = false;
			}
			if (Input.GetMouseButton (1)) {

				if (GlobalControl.baseCount == 0){
					warningTxt.text = "No Base, You need to build observatory first";
					buildAvailable = false;
				}

				if (GlobalControl.currency < 7000){
					warningTxt.text = "No enough fund";
				}

				if (GlobalControl.baseCount > 0 && GlobalControl.currency > 7000.0f){
					genHold = false;
					GameObject genBuilt = GameObject.Instantiate(generator, generator.transform.position, generator.transform.rotation) as GameObject;
					genBuilt.name = "Generator";

					Vector3 pos = genBuilt.transform.position;
					pos.y = Terrain.activeTerrain.SampleHeight (genBuilt.transform.position)-12.0f;
					genBuilt.transform.position = pos;

					if (Physics.Raycast (genBuilt.transform.position, -Vector3.up, out hits, 5)) {
						currentNormalG = Vector3.Lerp (currentNormalG, hits.normal, 4 * Time.deltaTime);
						genBuilt.transform.up = hits.normal;
					}

					generator.GetComponent<Rigidbody> ().isKinematic = false;
					generator.SetActive (false);
					menuNum = 0;
					tip.SetActive (false);
					GlobalControl.genCount += 1;
					GlobalControl.currency -= 7000;

					genAlreadyBuilt = true;
				}
			}
		}

		if (sateHold == true) {
			satellite.SetActive (true);
			satellite.transform.position = charInfo.transform.position;
			satellite.transform.Translate(0,0,-30);
			satellite.GetComponent<Rigidbody> ().isKinematic = true;
			tip.SetActive (true);

			Vector3 pos2 = satellite.transform.position;
			pos2.y = Terrain.activeTerrain.SampleHeight (satellite.transform.position)-12.0f;
			satellite.transform.position = pos2;

			if (Input.GetKeyDown (KeyCode.LeftAlt)) {
				satellite.SetActive (false);
				menuNum = 0;
				tip.SetActive (false);
				sateHold = false;
			}
			if (Input.GetMouseButton (1)) {

				if (GlobalControl.genCount == 0){
					warningTxt.text = "No Power, You need to build generator first";
				}

				if (GlobalControl.currency < 8000){
					warningTxt.text = "No enough fund";
				}

				if (GlobalControl.unitCount > GlobalControl.genCount * 5){
					warningTxt.text = "Power Insufficient, You need to build more generators";
				}

				if (GlobalControl.genCount > 0 && GlobalControl.unitCount <= GlobalControl.genCount * 5 && GlobalControl.currency > 8000.0f) {
					sateHold = false;
					GameObject sateBuilt = GameObject.Instantiate (satellite, satellite.transform.position, satellite.transform.rotation) as GameObject;
					sateBuilt.name = "Satellite";

					Vector3 pos = sateBuilt.transform.position;
					pos.y = Terrain.activeTerrain.SampleHeight (sateBuilt.transform.position) - 12.0f;
					sateBuilt.transform.position = pos;

					if (Physics.Raycast (sateBuilt.transform.position, -Vector3.up, out hits, 1000)) {
						currentNormalS = Vector3.Lerp (currentNormalS, hits.normal, 4 * Time.deltaTime);
						sateBuilt.transform.up = hits.normal;
					}

					satellite.GetComponent<Rigidbody> ().isKinematic = false;
					satellite.SetActive (false);
					menuNum = 0;
					tip.SetActive (false);
					GlobalControl.sateCount += 1;
					GlobalControl.currency -= 8000;

					sateAlreadyBuilt = true;
				}
			}
		}

		if (rhinoHold == true) {
			rhino.SetActive (true);
			rhino.transform.position = charInfo.transform.position;
			rhino.transform.Translate(0,0,-30);
			rhino.GetComponent<Rigidbody> ().isKinematic = true;
			tip.SetActive (true);

			Vector3 pos2 = rhino.transform.position;
			pos2.y = Terrain.activeTerrain.SampleHeight (rhino.transform.position)-10.0f;
			rhino.transform.position = pos2;

			if (Input.GetKeyDown (KeyCode.LeftAlt)) {
				rhino.SetActive (false);
				menuNum = 0;
				tip.SetActive (false);
				rhinoHold = false;
			}
			if (Input.GetMouseButton (1)) {
				if (GlobalControl.gameWon != true || GlobalControl.gameLost != true) {
					if (GlobalControl.genCount == 0) {
						warningTxt.text = "No Power, You need to build generator first";
					}

					if (GlobalControl.currency < 10000) {
						warningTxt.text = "No enough fund";
					}

					if (GlobalControl.unitCount > GlobalControl.genCount * 5) {
						warningTxt.text = "Power Insufficient, You need to build more generators";
					}

					if (GlobalControl.genCount > 0 && GlobalControl.unitCount <= GlobalControl.genCount * 5 && GlobalControl.currency >= 10000.0f) {
						rhinoHold = false;
						GameObject rhinoBuilt = GameObject.Instantiate (rhino, rhino.transform.position, rhino.transform.rotation) as GameObject;
						rhinoBuilt.name = "Rhino";
						rhinoBuilt.transform.position = rhino.transform.position;
						rhino.GetComponent<Rigidbody> ().isKinematic = false;
						rhino.SetActive (false);
						menuNum = 0;
						tip.SetActive (false);
						GlobalControl.rhinoCount += 1;
						GlobalControl.currency -= 10000;

						rhinoAlreadyBuilt = true;
					}
				}
			}
		}

		if (uavHold == true) {
			uav.SetActive (true);
			uav.transform.position = charInfo.transform.position;
			uav.GetComponent<Rigidbody> ().isKinematic = true;
			uav.transform.Translate(0,0,-30);
			tip.SetActive (true);

			Vector3 pos2 = uav.transform.position;
			pos2.y = Terrain.activeTerrain.SampleHeight (uav.transform.position);
			uav.transform.position = pos2;

			if (Input.GetKeyDown (KeyCode.LeftAlt)) {
				uav.SetActive (false);
				menuNum = 0;
				tip.SetActive (false);
				uavHold = false;
			}
			if (Input.GetMouseButton (1)) {
				if (GlobalControl.gameWon != true || GlobalControl.gameLost != true) {
					if (GlobalControl.genCount == 0) {
						warningTxt.text = "No Power, You need to build generator first";
					}

					if (GlobalControl.currency < 20000) {
						warningTxt.text = "No enough fund";
					}

					if (GlobalControl.unitCount > GlobalControl.genCount * 5) {
						warningTxt.text = "Power Insufficient, You need to build more generators";
					}

					if (GlobalControl.genCount > 0 && GlobalControl.unitCount <= GlobalControl.genCount * 5 && GlobalControl.currency >= 20000.0f) {
						uavHold = false;
						GameObject uavBuilt = GameObject.Instantiate (uav, uav.transform.position, uav.transform.rotation) as GameObject;
						uavBuilt.name = "UAV";
						uavBuilt.transform.position = uav.transform.position;
						uav.GetComponent<Rigidbody> ().isKinematic = false;
						uav.SetActive (false);
						menuNum = 0;
						tip.SetActive (false);
						GlobalControl.uavCount += 1;
						GlobalControl.currency -= 20000;

						uavAlreadyBuilt = true;
					}
				}
			}
		}

		//Initialize UI while pressing
		if (Input.GetKeyDown(KeyCode.LeftAlt) && menuNum == 0) {
			subui.SetActive(true);
			menuNum = 1;
		}

		//reset UI
		else if (Input.GetKeyDown (KeyCode.LeftAlt) && menuNum != 0){
			menuNum = 0;
			baseui.SetActive (false);
			genui.SetActive (false);
			uavui.SetActive (false);
			rhinoui.SetActive (false);
			sateui.SetActive (false);
			subui.SetActive (false);
		}

		else if (Input.GetKeyDown (KeyCode.Tab) && menuNum != 0){
			baseui.SetActive (false);
			genui.SetActive (false);
			uavui.SetActive (false);
			rhinoui.SetActive (false);
			sateui.SetActive (false);
			subui.SetActive (false);

			//enable buildings
			if (menuNum == 1) {
				baseHold = true;
			} 

			else if (menuNum == 2) {
				genHold = true;
			}

			else if (menuNum == 3) {
				sateHold = true;
			}

			else if (menuNum == 4) {
				rhinoHold = true;
			}

			else if (menuNum == 5) {
				uavHold = true;
			}
		}

		else if (menuNum != 0) {
			//switch items
			if (Input.GetKeyDown(KeyCode.E)){
				if (menuNum == 1) {
					menuNum = 2;
				}
				else if (menuNum == 2) {
					menuNum = 3;
				}
				else if (menuNum == 3) {
					menuNum = 4;
				}
				else if (menuNum == 4) {
					menuNum = 5;
				}
			}
			if (Input.GetKeyDown(KeyCode.Q)){
				if (menuNum == 5) {
					menuNum = 4;
				}
				else if (menuNum == 4) {
					menuNum = 3;
				}
				else if (menuNum == 3) {
					menuNum = 2;
				}
				else if (menuNum == 2) {
					menuNum = 1;
				}
			}

			if (menuNum == 1) {
				baseui.SetActive(true);
				genui.SetActive(false);
				uavui.SetActive(false);
				rhinoui.SetActive(false);
				sateui.SetActive(false);
			}

			if (menuNum == 2) {
				baseui.SetActive(false);
				genui.SetActive(true);
				uavui.SetActive(false);
				rhinoui.SetActive(false);
				sateui.SetActive(false);
			}

			if (menuNum == 3) {
				baseui.SetActive(false);
				genui.SetActive(false);
				uavui.SetActive(false);
				rhinoui.SetActive(false);
				sateui.SetActive(true);
			}

			if (menuNum == 4) {
				baseui.SetActive(false);
				genui.SetActive(false);
				uavui.SetActive(false);
				rhinoui.SetActive(true);
				sateui.SetActive(false);
			}

			if (menuNum == 5) {
				baseui.SetActive (false);
				genui.SetActive (false);
				uavui.SetActive (true);
				rhinoui.SetActive (false);
				sateui.SetActive (false);
			}

		}
	}
}
