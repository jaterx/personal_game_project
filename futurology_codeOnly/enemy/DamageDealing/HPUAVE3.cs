using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUAVE3 : MonoBehaviour {
	public static GameObject hpfeu3;
	public static GameObject hpneu3;
	public static RectTransform hpfeu3RT;
	public static float damageDealing;
	public static Text objInfoEme;
	public Rigidbody mainRigidBody;
	// Use this for initialization
	void Start () {
		hpfeu3 = GameObject.FindGameObjectWithTag ("HPFEU3");
		hpneu3 = GameObject.Find ("HPNEU3");
		hpfeu3RT = hpfeu3.GetComponent<RectTransform> ();
		damageDealing = 100.0f;
		objInfoEme = GameObject.FindGameObjectWithTag ("InfoPanelEmeObj").GetComponent<Text>();
		//objInfoEme.text = "Enemy Unit\nNothing Selected";
		mainRigidBody = this.GetComponent<Rigidbody>();
		mainRigidBody.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if ((int)hpfeu3RT.rect.width <= 0){
			hpfeu3RT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpneu3.transform.position = new Vector3 (99999,99999,99999);
			hpfeu3.transform.position = hpneu3.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.u3Killed = true;
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpneu3.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfeu3.transform.position = hpneu3.transform.position;
				if (Input.GetMouseButton(1)){
					hpneu3.transform.position = new Vector3 (99999,99999,99999);
					hpfeu3.transform.position = hpneu3.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "UAV\nFlying Unit\nHP " + (int)hpfeu3RT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpneu3.transform.position = new Vector3 (99999,99999,99999);
				hpfeu3.transform.position = hpneu3.transform.position;
			}
			if (Input.GetMouseButton (1)) {
				objInfoEme.text = "Enemy Unit\nNothing Selected";
			}
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			unitFocus.savedEnemyObjectForUI = this.gameObject;
			damageDealing -= 0.1f;
			hpfeu3RT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}


}
