using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RhinoPhysics : MonoBehaviour {
	public float rotationRange = 1.0f;
	public float minSpeed = 0.0f;
	public float maxSpeed = 15.0f;
	public static float carSpeed = 0.0f;
	public float maxHeight = 128.0f;
	public float minHeight = 30.0f;
	public float currentHeight;
	public float dropSpeed = 15.0f;
	public static bool slide;
	public static bool slideB;
	public float rotationX = 0;
	public float rotationXX = 0;
	public float rotationY = 190;

	private float carRotation = 0.0f;
	//private Vector3 pivot = new Vector3(0,0,0);

	public GameObject getChar;
	public GameObject getRhino;

	public static float acc;
	private Vector3 offset;
	public static Quaternion outRotation;

	public Text SpeedTxt;
	public Text SpeedUTxt;
	public GameObject SPT;
	public GameObject SPTU;
	public GameObject cmf;

	// Use this for initialization
	void Start () {
		acc = 4.0f;
		slide = false;
		slideB = false;
		getChar = GameObject.FindGameObjectWithTag ("Char");
		SPT = GameObject.FindGameObjectWithTag ("SPT");
		SpeedTxt = SPT.GetComponent<Text> ();
		SPTU = GameObject.FindGameObjectWithTag ("SPTU");
		SpeedUTxt = SPTU.GetComponent<Text> ();
		cmf = GameObject.FindGameObjectWithTag ("camRef");
	}
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (RideRhino.drivePass == true) {
			SpeedTxt.text = (Mathf.Abs((int)(carSpeed*5.2f))).ToString ();
			SpeedUTxt.text = "KM/H";
			getRhino = RideRhino.rhinoO;
			currentHeight = gameObject.transform.position.y;
			//carSpeed = minSpeed + (yPosition * (maxSpeed-minSpeed));

			if (carSpeed > maxSpeed){
				carSpeed = maxSpeed;
			}

			if (Input.GetKey (KeyCode.W)) {
				slide = false;
				if (carSpeed < maxSpeed) {
					carSpeed += 1.0f * Time.deltaTime * acc;
				}
				if (carSpeed < 0) {
					carSpeed += 1.0f * Time.deltaTime * acc * 3;
				}
				//getRhino.transform.position += getRhino.transform.forward * carSpeed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.S)) {
				slideB = false;
				if (Mathf.Abs (carSpeed) < maxSpeed) {
					carSpeed -= 1.0f * Time.deltaTime * acc * 3;
				}
				//getRhino.transform.position += getRhino.transform.forward * carSpeed * Time.deltaTime;
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
				//getRhino.transform.position += getRhino.transform.forward * carSpeed * Time.deltaTime;
			}
			if(slideB == true){
				carSpeed += 1.0f * Time.deltaTime * acc;
				if (carSpeed >= 0.0f){
					slideB = false;
				}
				//getRhino.transform.position += getRhino.transform.forward * carSpeed * Time.deltaTime;
			}
			getRhino.transform.position += getRhino.transform.forward * carSpeed * Time.deltaTime;
			//MapMagicDemo.CameraController.accessChar.transform.RotateAround(this.gameObject.transform.position,this.transform.up,1.0f);
			getChar.transform.position = new Vector3(getRhino.transform.position.x + 12.5f, getRhino.transform.position.y + 4.0f, getRhino.transform.position.z - 15f);
			rotationY += Input.GetAxis("Mouse X")*1.0f;
			rotationXX -= Input.GetAxis("Mouse Y")*1.0f;
			getRhino.transform.eulerAngles = new Vector3(0, rotationY, 0);
			outRotation = Quaternion.Euler (getRhino.transform.eulerAngles);
			getChar.transform.RotateAround (getRhino.transform.position,getRhino.transform.up, rotationY);
			getChar.transform.RotateAround (getRhino.transform.position,getRhino.transform.right , -rotationXX);
			getChar.transform.LookAt (getRhino.transform);

			//prevent go through terrain when driving
			Vector3 pos = getRhino.transform.position;
			pos.y = Terrain.activeTerrain.SampleHeight (getRhino.transform.position);
			getRhino.transform.position = new Vector3(pos.x, pos.y-10.3f, pos.z);
			//getRhino.transform.up = cmf.transform.up;

		}
		if (RideRhino.drivePass == false && rideUAV.drivePass == false) {
			rotationX = 0;
			rotationY = 190;
			getChar.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
			SpeedTxt.text = " ";
			SpeedUTxt.text = " ";
		}
	}
}