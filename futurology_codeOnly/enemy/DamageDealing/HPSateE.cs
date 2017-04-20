using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSateE : MonoBehaviour {
	public static GameObject hpfes;
	public static GameObject hpnes;
	public static RectTransform hpfesRT;
	public static float damageDealing;
	public static Text objInfoEme;
	// Use this for initialization
	public Rigidbody mainRigidBody;
	public Rigidbody dishRigidBody;

	public static GameObject getThis;

	void Start () {
		hpfes = GameObject.FindGameObjectWithTag ("HPFES");
		hpnes = GameObject.Find ("HPNES");
		hpfesRT = hpfes.GetComponent<RectTransform> ();
		damageDealing = 100.0f;
		objInfoEme = GameObject.FindGameObjectWithTag ("InfoPanelEmeObj").GetComponent<Text>();
		mainRigidBody = GameObject.FindGameObjectWithTag ("SupportE").GetComponent<Rigidbody> ();
		dishRigidBody = GameObject.FindGameObjectWithTag ("DishE").GetComponent<Rigidbody> ();
		//objInfoEme.text = "Enemy Unit\nNothing Selected";
		mainRigidBody.useGravity = false;
		dishRigidBody.useGravity = false;
		getThis = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		if ((int)hpfesRT.rect.width <= 50){
			dishRigidBody.useGravity = true;
		}
		if ((int)hpfesRT.rect.width <= 0){
			hpfesRT.sizeDelta = new Vector2 (0,5);
			mainRigidBody.useGravity = true;
			hpnes.transform.position = new Vector3 (99999,99999,99999);
			hpfes.transform.position = hpnes.transform.position;
			this.GetComponent<BoxCollider> ().enabled = false;
			GlobalControl.sKilled = true;
		}

		if (MapMagicDemo.CameraController.controlMode == 1){
			if (unitFocus.savedEnemyObjectForUI == this.gameObject){
				hpnes.transform.position = unitFocus.isoCam.WorldToScreenPoint (new Vector3(this.transform.position.x-15.0f, this.transform.position.y + 10.0f, this.transform.position.z));
				hpfes.transform.position = hpnes.transform.position;
				if (Input.GetMouseButton(1)){
					hpnes.transform.position = new Vector3 (99999,99999,99999);
					hpfes.transform.position = hpnes.transform.position;
					unitFocus.savedEnemyObjectForUI = null;
					//objInfoEme.text = "Enemy Unit\nNothing Selected";
				}
				objInfoEme.text = "Satellite\nBuilding\nHP " + (int)hpfesRT.rect.width + "%";
			}
			if (unitFocus.savedEnemyObjectForUI != this.gameObject) {
				hpnes.transform.position = new Vector3 (99999,99999,99999);
				hpfes.transform.position = hpnes.transform.position;
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
					hpfesRT.sizeDelta = new Vector2 (damageDealing,5);
				}
			}
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("UAVBullet")){
			unitFocus.savedEnemyObjectForUI = this.gameObject;
			damageDealing -= 0.02f;
			hpfesRT.sizeDelta = new Vector2 (damageDealing,5);
		}
	}


}
