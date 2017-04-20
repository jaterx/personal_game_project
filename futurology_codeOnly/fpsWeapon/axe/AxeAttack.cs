using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack : MonoBehaviour {

	Animator anim;
	public static float attack;
	public static bool moved;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		attack = 2.0f;
		moved = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (MapMagicDemo.CameraController.controlMode == 0){
			if (Input.GetMouseButton(1)){
				attack = 3.0f;
			}
			if (!Input.GetMouseButton(1)){
				attack = 1.0f;
			}
			anim.SetFloat ("Attack", attack);
		}

	}
}
