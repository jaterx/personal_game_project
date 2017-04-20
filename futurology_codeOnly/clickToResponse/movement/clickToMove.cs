using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickToMove : MonoBehaviour {

	//public Vector3 originalPos;
	public Vector3 targetPos;
	public static bool objectSelected;
	public static bool moveConfirmed;
	public static bool changeDirection;
	public float speed;
	public float speedDt;
	public static GameObject savedObject;
	public static Vector3 savedDirection;
	public static int flag;
	public float xDeg;
	public float yDeg;
	public Quaternion fromR;
	public Quaternion toR;
	RaycastHit hit;
	public AudioSource rhinoSFX;

	// Use this for initialization
	void Start () {
		objectSelected = false;
		moveConfirmed = false;
		changeDirection = false;
		speed = 1.0f;
		//originalPos = transform.position;
		flag = 0;
		rhinoSFX = this.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		Ray ray2 = unitFocus.isoCam.ScreenPointToRay (Input.mousePosition);
		ray2.origin = unitFocus.camRef.transform.position;
		Debug.DrawRay ( ray2.origin, ray2.direction*500 , Color.red);

		if (unitFocus.focusMode == false){
			unitFocus.indicator1.SetActive (true);
			unitFocus.indicator = unitFocus.indicator1;
			unitFocus.indicator2.SetActive (false);
		}
		if (unitFocus.focusMode == true){
			unitFocus.indicator2.SetActive (true);
			unitFocus.indicator = unitFocus.indicator2;
			unitFocus.indicator1.SetActive (false);
		}

		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray2, out hit, 10000)) {
				//newName = hit.transform.name;
				if (hit.transform.name == "Rhino") {
					savedObject = hit.collider.gameObject;
					objectSelected = true;
					rhinoSFX.Play ();
				}
			}
		}

		if (objectSelected == true) {

			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (ray2, out hit, 10000)) {
					if (hit.transform.name != "Rhino") {
						unitFocus.indicator.transform.position = Input.mousePosition;
						moveConfirmed = true;
						targetPos = hit.point;
					}
					if(hit.transform.name == "UAV" || hit.transform.name == "Observatory Base" || hit.transform.name == "Generator" || hit.transform.name == "Satellite"){
						objectSelected = false;
						moveConfirmed = false;
						changeDirection = false;
						unitFocus.indicator.transform.position = new Vector3 (99999, 99999, 99999);
						unitFocus.uiTracker = false;
						flag = 1;
					}
				}
			}
		}

		speedDt = speed * Time.deltaTime;

		if (moveConfirmed == true){
			if (unitFocus.uiTracker == true) {
				savedObject.transform.LookAt (new Vector3 (hit.point.x, savedObject.transform.position.y, hit.point.z), unitFocus.camRef.transform.up);
				savedObject.transform.position = Vector3.MoveTowards (savedObject.transform.position, targetPos, speed);
				if (savedObject.transform.position == targetPos) {
					flag = 2;
					objectSelected = false;
					moveConfirmed = false;
					unitFocus.indicator.transform.position = new Vector3 (99999, 99999, 99999);
					unitFocus.uiTracker = false;
				} else {
					flag = 1;
				}
			}
		}


		if (Input.GetMouseButtonDown(1)){
			objectSelected = false;
			moveConfirmed = false;
			changeDirection = false;
			unitFocus.indicator.transform.position = new Vector3 (99999, 99999, 99999);
			unitFocus.uiTracker = false;
			flag = 1;
		}
	}
}
