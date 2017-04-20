using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonChar : MonoBehaviour {
	public GameObject cam;
	public GameObject camMain;
	public GameObject playerRef;
	public GameObject terrainS;
	public static bool charSwitch;
	public float rotationX = 0;
	public float rotationXX = 0;
	public float rotationY = 190;
	public static Quaternion outRotation;
	public static float walkVar;
	public static float jumpVar;
	public static float runVar;
	Animator anim;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("Char");
		camMain = GameObject.FindGameObjectWithTag ("MainCamera");
		playerRef = GameObject.FindGameObjectWithTag ("PlayerRef");
		terrainS = GameObject.FindGameObjectWithTag ("Terrain");
		charSwitch = false;
		this.transform.eulerAngles = new Vector3 (0.0f,180.0f,0.0f);
		anim = GetComponent<Animator>();
		cam.transform.position = terrainS.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (Input.GetKey (KeyCode.X)) {
			
			charSwitch = true;
		}
		if (Input.GetKey (KeyCode.Z)) {
			charSwitch = false;
		}

		//float yRotation = camMain.transform.eulerAngles.y;
		//this.transform.eulerAngles = new Vector3( 0.0f, this.transform.eulerAngles.y, 0.0f);
		//rotationY += Input.GetAxis("Mouse X")*1.0f;
		//this.transform.RotateAround (camMain.transform.position,camMain.transform.up, 0);
		//MapMagicDemo.CameraController.accessChar.transform.RotateAround(this.gameObject.transform.position,this.transform.up,1.0f);

		if (MapMagicDemo.CameraController.controlMode == 0){
			if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) {
				runVar = 3.0f;
			}
			if (Input.GetKey(KeyCode.W)){
				walkVar = 3.0f;
			}
			if (Input.GetKey(KeyCode.S)){
				walkVar = 3.0f;
			}
			if (Input.GetKeyDown(KeyCode.Space)){
				jumpVar = 3.0f;
			}
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.W)) {
				runVar = 1.0f;
			}
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S)) {
				runVar = 1.0f;
			}
			if (!Input.GetKey(KeyCode.W)) {
				walkVar = 1.0f;
			}
			if (!Input.GetKey(KeyCode.Space)) {
				jumpVar = 1.0f;
			}

			this.transform.position = cam.transform.position;
			this.transform.eulerAngles = new Vector3 (0, camMain.transform.eulerAngles.y, 0);

		}
		anim.SetFloat ("Walk", walkVar);
		anim.SetFloat ("Jump", jumpVar);
		anim.SetFloat ("Run", runVar);

	}
}
