using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPRhinoE4 : MonoBehaviour {
	public static GameObject hpfer4;
	public static GameObject hpner4;
	public static RectTransform hpfer4RT;
	public static float damageDealing;
	public static Text objInfoEme;
	// Use this for initialization
	public Rigidbody mainRigidBody;
	public static bool damaged;
	void Start () {
		hpfer4 = GameObject.FindGameObjectWithTag ("HPFER4");
		hpner4 = GameObject.Find ("HPNER4");
		hpfer4RT = hpfer4.GetComponent<RectTransform> ();
		damageDealing = 100.0f;
		objInfoEme = GameObject.FindGameObjectWithTag ("InfoPanelEmeObj").GetComponent<Text>();
		//objInfoEme.text = "Enemy Unit\nNothing Selected";
		mainRigidBody = this.GetComponent<Rigidbody>();
		mainRigidBody.useGravity = false;
		damaged = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if ((int)hpfer4RT.rect.width <= 0){
			hpfer4RT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpner4.transform.position = new Vector3 (99999,99999,99999);
			hpfer4.transform.position = hpner4.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.r4Killed = true;
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpner4.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfer4.transform.position = hpner4.transform.position;
				if (Input.GetMouseButton(1)){
					hpner4.transform.position = new Vector3 (99999,99999,99999);
					hpfer4.transform.position = hpner4.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "Rhino\nGround Unit\nHP " + (int)hpfer4RT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpner4.transform.position = new Vector3 (99999,99999,99999);
				hpfer4.transform.position = hpner4.transform.position;
			}
			if (Input.GetMouseButton (1)) {
				objInfoEme.text = "Enemy Unit\nNothing Selected";
			}
		}

		foreach (GameObject friendlyRhino in GameObject.FindGameObjectsWithTag ("Rhino")) {
			float distanceTR = Vector3.Distance (friendlyRhino.transform.position, this.transform.position);
			if (distanceTR <= 30){
				if (FireAttack.fireSpelled == true){
					unitFocus.savedEnemyObjectForUI = this.gameObject;
					damageDealing -= 0.04f;
					damaged = true;
					hpfer4RT.sizeDelta = new Vector2 (damageDealing,5);
				}
			}
			if (distanceTR > 30){
				damaged = false;
			}
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			unitFocus.savedEnemyObjectForUI = this.gameObject;
			damageDealing -= 0.2f;
			damaged = true;
			hpfer4RT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			damaged = false;
		}
	}
}
