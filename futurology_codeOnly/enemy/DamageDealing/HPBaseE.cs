using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBaseE : MonoBehaviour {
	public static GameObject hpfeb;
	public static GameObject hpneb;
	public static RectTransform hpfebRT;
	public static float damageDealing;
	public static Text objInfoEme;
	public Rigidbody mainRigidBody;
	public static GameObject getThis;
	// Use this for initialization
	void Start () {
		hpfeb = GameObject.FindGameObjectWithTag ("HPFEB");
		hpneb = GameObject.Find ("HPNEB");
		hpfebRT = hpfeb.GetComponent<RectTransform> ();
		damageDealing = 100.0f;
		objInfoEme = GameObject.FindGameObjectWithTag ("InfoPanelEmeObj").GetComponent<Text>();
		//objInfoEme.text = "Enemy Unit\nNothing Selected";
		mainRigidBody = this.GetComponent<Rigidbody>();
		mainRigidBody.useGravity = false;

		getThis = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if ((int)hpfebRT.rect.width <= 0){
			hpfebRT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpneb.transform.position = new Vector3 (99999,99999,99999);
			hpfeb.transform.position = hpneb.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.bKilled = true;
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpneb.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfeb.transform.position = hpneb.transform.position;
				if (Input.GetMouseButton(1)){
					hpneb.transform.position = new Vector3 (99999,99999,99999);
					hpfeb.transform.position = hpneb.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "Observatory\nBuilding\nHP " + (int)hpfebRT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpneb.transform.position = new Vector3 (99999,99999,99999);
				hpfeb.transform.position = hpneb.transform.position;
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
					hpfebRT.sizeDelta = new Vector2 (damageDealing,5);
				}
			}
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			unitFocus.savedEnemyObjectForUI = this.gameObject;
			damageDealing -= 0.02f;
			hpfebRT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}


}
