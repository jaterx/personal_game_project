using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetSound : MonoBehaviour {

	public AudioSource jetSFX;

	// Use this for initialization
	void Start () {
		jetSFX = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (carPhysics.shiftPressed == true){
			jetSFX.Play ();
		}
		if (carPhysics.shiftPressed == false){
			jetSFX.Stop ();
		}
	}
}
