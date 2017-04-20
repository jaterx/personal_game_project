using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPRhinoE2 : MonoBehaviour {
	public static GameObject hpfer2;
	public static GameObject hpner2;
	public static RectTransform hpfer2RT;
	public static float damageDealing;
	public static Text objInfoEme;
	public static bool damaged;
	// Use this for initialization
	public Rigidbody mainRigidBody;
	void Start () {
		hpfer2 = GameObject.FindGameObjectWithTag ("HPFER2");
		hpner2 = GameObject.Find ("HPNER2");
		hpfer2RT = hpfer2.GetComponent<RectTransform> ();
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
		if ((int)hpfer2RT.rect.width <= 0){
			hpfer2RT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpner2.transform.position = new Vector3 (99999,99999,99999);
			hpfer2.transform.position = hpner2.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.r2Killed = true;
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpner2.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfer2.transform.position = hpner2.transform.position;
				if (Input.GetMouseButton(1)){
					hpner2.transform.position = new Vector3 (99999,99999,99999);
					hpfer2.transform.position = hpner2.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "Rhino\nGround Unit\nHP " + (int)hpfer2RT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpner2.transform.position = new Vector3 (99999,99999,99999);
				hpfer2.transform.position = hpner2.transform.position;
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
					hpfer2RT.sizeDelta = new Vector2 (damageDealing,5);
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
			hpfer2RT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			damaged = false;
		}
	}

}
