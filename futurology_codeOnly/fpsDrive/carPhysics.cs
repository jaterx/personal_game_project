using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class carPhysics : MonoBehaviour {
	public float rotationRange = 1.0f;
	public float minSpeed = 0.0f;
	public float maxSpeed = 25.0f;
	public static float carSpeed = 0.0f;
	public static float tempMaxSpeed = 40.0f;
	public float maxHeight = 128.0f;
	public float minHeight = 30.0f;
	public float currentHeight;
	public float dropSpeed = 15.0f;
	public static bool slide;
	public static bool slideB;
	public static bool slideBoost;
	public float rotationX = 0;
	public float rotationY = 190;

	private float carRotation = 0.0f;
	//private Vector3 pivot = new Vector3(0,0,0);

	public GameObject getChar;
	public GameObject getUAV;

	public Text SpeedTxt;
	public Text SpeedUTxt;
	public GameObject SPT;
	public GameObject SPTU;

	public static bool wPressed;
	public static bool sPressed;
	public static bool shiftPressed;

	public static float acc;
	public static bool checkAcc;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		acc = 4.0f;
		slide = false;
		slideB = false;
		slideBoost = false;
		wPressed = false;
		sPressed = false;
		shiftPressed = false;
		getChar = GameObject.FindGameObjectWithTag ("Char");

		//boostParticle = GameObject.FindGameObjectWithTag ("BoostSpeed").GetComponent<ParticleSystem>();

		SPT = GameObject.FindGameObjectWithTag ("SPT");
		SpeedTxt = SPT.GetComponent<Text> ();
		SPTU = GameObject.FindGameObjectWithTag ("SPTU");
		SpeedUTxt = SPTU.GetComponent<Text> ();
		checkAcc = false;
	}
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (rideUAV.drivePass == true) {

			//boost speed
			if (Input.GetKeyDown (KeyCode.LeftShift) && Input.GetKey (KeyCode.W)) {
				acc = 12.0f;
				maxSpeed = tempMaxSpeed;
				checkAcc = true;
				slideBoost = false;
				shiftPressed = true;
			}
			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				acc = 4.0f;
				slideBoost = true;
				shiftPressed = false;
			}

			SpeedTxt.text = (Mathf.Abs((int)(carSpeed*5.2f))).ToString ();
			SpeedUTxt.text = "KM/H";
			getUAV = rideUAV.uavO;
			currentHeight = gameObject.transform.position.y;
			//carSpeed = minSpeed + (yPosition * (maxSpeed-minSpeed));

			if (carSpeed > maxSpeed){
				carSpeed = maxSpeed;
			}

			if (Input.GetKey (KeyCode.W)) {
				wPressed = true;
				sPressed = false;
				slide = false;
				if (carSpeed < maxSpeed) {
					carSpeed += 1.0f * Time.deltaTime * acc;
				}
				if (carSpeed < 0) {
					carSpeed += 1.0f * Time.deltaTime * acc * 3;
				}
				if (carSpeed < 25.0f || maxSpeed == tempMaxSpeed) {
					checkAcc = true;
				}
				if (carSpeed >= 25.0f) {
					checkAcc = false;
				}
				//getUAV.transform.position += getUAV.transform.forward * carSpeed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.S)) {
				wPressed = false;
				sPressed = true;
				slideB = false;
				checkAcc = false;

				if (Mathf.Abs (carSpeed) < maxSpeed) {
					carSpeed -= 1.0f * Time.deltaTime * acc * 3;
				}
			}
			if(Input.GetKeyUp(KeyCode.S)){
				slideB = true;
			}
			if(Input.GetKeyUp(KeyCode.W)){
				slide = true;
			}
			if(slide == true){
				carSpeed -= 1.0f * Time.deltaTime * acc;
				if (carSpeed <= 0.0f){
					slide = false;
				}
				wPressed = false;
				sPressed = false;
			}
			if(slideB == true){
				carSpeed += 1.0f * Time.deltaTime * acc;
				if (carSpeed >= 0.0f){
					slideB = false;
				}
				wPressed = false;
				sPressed = false;
			}
			if(slideBoost == true){
				carSpeed -= 2.0f * Time.deltaTime * acc;
				if (carSpeed <= 25.0f){
					slideBoost = false;
					maxSpeed = 25.0f;
				}
			}
			getUAV.transform.position += getUAV.transform.forward * carSpeed * Time.deltaTime;
			//MapMagicDemo.CameraController.accessChar.transform.RotateAround(this.gameObject.transform.position,this.transform.up,1.0f);
			getChar.transform.position = new Vector3(getUAV.transform.position.x, getUAV.transform.position.y + 4.0f, getUAV.transform.position.z - 20.0f);
			MapMagicDemo.CameraController.accessChar.GetComponent<MapMagicDemo.CharController> ().gravity = false;
			rotationY += Input.GetAxis("Mouse X")*1.0f;
			rotationX -= Input.GetAxis("Mouse Y")*1.0f;
			getUAV.transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
			getChar.transform.RotateAround (getUAV.transform.position,getUAV.transform.up, rotationY);
			getChar.transform.RotateAround (getUAV.transform.position,getUAV.transform.right, rotationX);
			getChar.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
			getChar.transform.LookAt (getUAV.transform);

			//prevent fly through terrain
			Vector3 pos = getUAV.transform.position;
			pos.y = Terrain.activeTerrain.SampleHeight (getUAV.transform.position) + 5.0f;
			if (getUAV.transform.position.y < pos.y){
				getUAV.transform.position = pos;
			}

		}
		if (rideUAV.drivePass == false && RideRhino.drivePass == false) {
			rotationX = 0;
			rotationY = 190;
			getChar.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
			SpeedTxt.text = " ";
			SpeedUTxt.text = " ";
		}
	}
}