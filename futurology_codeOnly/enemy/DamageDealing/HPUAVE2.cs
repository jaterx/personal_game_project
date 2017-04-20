using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUAVE2 : MonoBehaviour {
	public static GameObject hpfeu2;
	public static GameObject hpneu2;
	public static RectTransform hpfeu2RT;
	public static float damageDealing;
	public static Text objInfoEme;
	public Rigidbody mainRigidBody;
	// Use this for initialization
	void Start () {
		hpfeu2 = GameObject.FindGameObjectWithTag ("HPFEU2");
		hpneu2 = GameObject.Find ("HPNEU2");
		hpfeu2RT = hpfeu2.GetComponent<RectTransform> ();
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

		if ((int)hpfeu2RT.rect.width <= 0){
			hpfeu2RT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpneu2.transform.position = new Vector3 (99999,99999,99999);
			hpfeu2.transform.position = hpneu2.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.u2Killed = true;
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpneu2.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfeu2.transform.position = hpneu2.transform.position;
				if (Input.GetMouseButton(1)){
					hpneu2.transform.position = new Vector3 (99999,99999,99999);
					hpfeu2.transform.position = hpneu2.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "UAV\nFlying Unit\nHP " + (int)hpfeu2RT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpneu2.transform.position = new Vector3 (99999,99999,99999);
				hpfeu2.transform.position = hpneu2.transform.position;
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
			hpfeu2RT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}


}
