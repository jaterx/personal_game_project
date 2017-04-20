using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickToFly : MonoBehaviour {

	//public Vector3 originalPos;
	public Vector3 targetPos;
	public static bool objectSelected;
	public static bool moveConfirmed;
	public float speed;
	public float speedDt;
	public static Camera isoCam2;
	public static GameObject camReff2;
	public static GameObject savedObject;
	public static int flag;
	RaycastHit hit;
	public AudioSource uavSFX;

	// Use this for initialization
	void Start () {
		objectSelected = false;
		moveConfirmed = false;
		speed = 1.0f;
		//originalPos = transform.position;
		isoCam2 = isoSwitch.accessIso.GetComponent<Camera>();
		camReff2 = isoSwitch.accessCMR;
		flag = 0;
		uavSFX = this.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		Ray ray = isoCam2.ScreenPointToRay (Input.mousePosition);
		ray.origin = camReff2.transform.position;
		Debug.DrawRay ( ray.origin, ray.direction*500 , Color.red);

		if (unitFocus.focusMode == false){
			unitFocus.indicator = unitFocus.indicator1;
			unitFocus.indicator2.transform.position = new Vector3 (99999, 99999, 99999);
		}
		if (unitFocus.focusMode == true){
			unitFocus.indicator = unitFocus.indicator2;
			unitFocus.indicator1.transform.position = new Vector3 (99999, 99999, 99999);
		}

		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray, out hit, 10000)) {
				//newName = hit.transform.name;
				if (hit.transform.name == "UAV") {
					savedObject = hit.collider.gameObject;
					objectSelected = true;
					uavSFX.Play ();
				}
			}
		}

		if (objectSelected == true) {
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (ray, out hit, 10000)) {
					if (hit.transform.name != "UAV") {
						unitFocus.indicator.transform.position = Input.mousePosition;
						moveConfirmed = true;
						targetPos.x = hit.point.x;
						targetPos.y = hit.point.y + 10.0f;
						targetPos.z = hit.point.z;
					}
					if(hit.transform.name == "Rhino" || hit.transform.name == "Observatory Base" || hit.transform.name == "Generator" || hit.transform.name == "Satellite"){
						objectSelected = false;
						moveConfirmed = false;
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
			unitFocus.indicator.transform.position = new Vector3 (99999, 99999, 99999);
			unitFocus.uiTracker = false;
			flag = 1;
		}

	}
}
