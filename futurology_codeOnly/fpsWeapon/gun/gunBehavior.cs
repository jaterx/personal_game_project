using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gunBehavior : MonoBehaviour {

	public float angle;
	public float period;

	private float ttime;

	public float degree;

	public GameObject bullet;

	public Vector3 shootPos;
	public static Vector3 originalPos;

	public static bool lockCursor = true;
	public static bool shot = false;

	void Start(){
		angle = 1.5f;
		period = 1;
		ttime = 1;
		bullet = GameObject.FindWithTag("Bullet");
	}

	// Update is called once per frame
	void Update () {
		//Cursor.lockState = CursorLockMode.Locked;
		//variables need to be constantly updated
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		ttime = ttime + Time.deltaTime;
		float phase = Mathf.Sin (ttime / period);

		shootPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		//variables need to be conditionally updated
		if (Input.GetKey ("w") || Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d")) {
			angle = 2;
			if (Input.GetKey (KeyCode.LeftShift)) {
				angle = 4;
			}
			if (Input.anyKey == false) {
				angle = 1.5f;
			}
		}
		if (Input.GetAxis ("Mouse X") < 0) {
			if (degree > -5) {
				degree = degree - 0.1f;
			}
		}
		if (Input.GetAxis ("Mouse X") > 0) {
			if (degree < 5) {
				degree = degree + 0.1f;
			}
		}
		if (Input.GetAxis ("Mouse X") == 0) {
			if (degree > 0) {
				degree = degree - 0.2f;
			}
			if (degree < 0) {
				degree = degree + 0.2f;
			}
		}
		if (Input.GetMouseButtonDown (0) || Input.GetMouseButton(0)) {
			GameObject bulletShot = GameObject.Instantiate(bullet, shootPos, transform.rotation*transform.localRotation) as GameObject;
			bulletShot.transform.Translate (-0.1f, 0.2f, 0.4f);
			bulletShot.transform.Rotate (0, -15, 0);
			originalPos = bulletShot.transform.position;
			GameObject.Destroy(bulletShot, 3f);

			shot = true;
		}
		if (!(Input.GetMouseButtonDown (0)) && (!Input.GetMouseButton (0))) {
			shot = false;
		}


		transform.localRotation = Quaternion.Euler (new Vector3 (phase * angle / 2, 188 + (phase * angle / 2), phase * angle / 2));
		transform.Rotate (0, 0, degree);
		//Debug.Log (degree);
	}
}