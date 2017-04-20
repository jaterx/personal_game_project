using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUAVE : MonoBehaviour {
	public static GameObject hpfeu;
	public static GameObject hpneu;
	public static RectTransform hpfeuRT;
	public static float damageDealing;
	public static float damageDealingPassed;
	public static Text objInfoEme;
	// Use this for initialization
	public Rigidbody mainRigidBody;

	void Start () {
		hpfeu = GameObject.FindGameObjectWithTag ("HPFEU");
		hpneu = GameObject.Find ("HPNEU");
		hpfeuRT = hpfeu.GetComponent<RectTransform> ();
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

		if ((int)hpfeuRT.rect.width <= 0){
			hpfeuRT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpneu.transform.position = new Vector3 (99999,99999,99999);
			hpfeu.transform.position = hpneu.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.u1Killed = true;
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpneu.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfeu.transform.position = hpneu.transform.position;
				if (Input.GetMouseButton(1)){
					hpneu.transform.position = new Vector3 (99999,99999,99999);
					hpfeu.transform.position = hpneu.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "UAV\nFlying Unit\nHP " + (int)hpfeuRT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpneu.transform.position = new Vector3 (99999,99999,99999);
				hpfeu.transform.position = hpneu.transform.position;
			}
			if (Input.GetMouseButton (1)) {
				objInfoEme.text = "Enemy Unit\nNothing Selected";
			}
		}

		damageDealingPassed = damageDealing;

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			unitFocus.savedEnemyObjectForUI = this.gameObject;
			damageDealing -= 0.1f;
			hpfeuRT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}


}
