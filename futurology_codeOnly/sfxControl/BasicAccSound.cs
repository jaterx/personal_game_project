using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAccSound : MonoBehaviour {

	public AudioSource accSFX;

	// Use this for initialization
	void Start () {
		accSFX = this.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		
		if (carPhysics.sPressed == true){
			accSFX.Play ();
		}
		if (carPhysics.sPressed == false){
			accSFX.Stop ();
		}
		if (carPhysics.wPressed == true){
			accSFX.Play ();
		}
		if (carPhysics.wPressed == false){
			accSFX.Stop ();
		}
	}
}
