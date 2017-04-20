using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGenE : MonoBehaviour {
	public static GameObject hpfeg;
	public static GameObject hpneg;
	public static RectTransform hpfegRT;
	public static float damageDealing;
	public static Text objInfoEme;
	public Rigidbody mainRigidBody;
	public Rigidbody bladeRigidBody;
	public Rigidbody headRigidBody;
	public static GameObject getThis;
	// Use this for initialization

	void Start () {
		hpfeg = GameObject.FindGameObjectWithTag ("HPFEG");
		hpneg = GameObject.Find ("HPNEG");
		hpfegRT = hpfeg.GetComponent<RectTransform> ();
		damageDealing = 100.0f;
		objInfoEme = GameObject.FindGameObjectWithTag ("InfoPanelEmeObj").GetComponent<Text>();
		//objInfoEme.text = "Enemy Unit\nNothing Selected";
		mainRigidBody = GameObject.FindGameObjectWithTag("MainE").GetComponent<Rigidbody>();
		headRigidBody = GameObject.FindGameObjectWithTag("HeadE").GetComponent<Rigidbody>();
		bladeRigidBody = GameObject.FindGameObjectWithTag("BladesE").GetComponent<Rigidbody>();
		mainRigidBody.useGravity = false;
		headRigidBody.useGravity = false;
		bladeRigidBody.useGravity = false;
		getThis = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if ((int)hpfegRT.rect.width <= 60){
			bladeRigidBody.useGravity = true;
		}
		if ((int)hpfegRT.rect.width <= 30){
			headRigidBody.useGravity = true;
		}
		if ((int)hpfegRT.rect.width <= 0){
			hpfegRT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpneg.transform.position = new Vector3 (99999,99999,99999);
			hpfeg.transform.position = hpneg.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.g1Killed = true;

		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpneg.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfeg.transform.position = hpneg.transform.position;
				if (Input.GetMouseButton(1)){
					hpneg.transform.position = new Vector3 (99999,99999,99999);
					hpfeg.transform.position = hpneg.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "Generator\nBuilding\nHP " + (int)hpfegRT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpneg.transform.position = new Vector3 (99999,99999,99999);
				hpfeg.transform.position = hpneg.transform.position;
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
					hpfegRT.sizeDelta = new Vector2 (damageDealing,5);
				}
			}
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			unitFocus.savedEnemyObjectForUI = this.gameObject;
			damageDealing -= 0.03f;
			hpfegRT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}


}
