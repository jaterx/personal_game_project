using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneralBehavior : MonoBehaviour {

	public static Camera isoCam;

	// Use this for initialization
	void Start () {
		isoCam = GameObject.FindGameObjectWithTag ("IsoCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
	}
}
