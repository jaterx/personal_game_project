using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public GameObject baseE;
	/*public float distance;
	public float referenceHP;
	public float damagedHP;
	public static GameObject ehpf;
	public static GameObject ehpn;*/

	// Use this for initialization
	void Start () {
		//ehpf = GameObject.FindGameObjectWithTag ("EHPF");
		//ehpn = GameObject.FindGameObjectWithTag ("EHPN");
		//referenceHP = ehpf.GetComponent<RectTransform> ().rect.width;
		//damagedHP = ehpn.GetComponent<RectTransform> ().rect.width;
		//ehpf.transform.position = EnemyGeneralBehavior.isoCam.WorldToScreenPoint (this.transform.position);
		//ehpf.transform.position = new Vector3 (ehpf.transform.position.x, ehpf.transform.position.y+5.0f, ehpf.transform.position.z);
		//ehpn.transform.position = ehpf.transform.position;
		baseE = GameObject.Find ("BaseE");
	}

	void OnCollisionEnter (Collision enemyCollider) {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		if(enemyCollider.gameObject.name == "BaseE"){
			Destroy (this.gameObject);
		}
	}
}
