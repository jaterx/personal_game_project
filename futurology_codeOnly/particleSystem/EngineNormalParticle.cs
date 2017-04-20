using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineNormalParticle : MonoBehaviour {

	ParticleSystem ps;
	ParticleSystem.EmissionModule em;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
		em = ps.emission;
		em.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (carPhysics.wPressed == true && carPhysics.sPressed == false && carPhysics.shiftPressed == false){
			em.enabled = true;
		}
		if (carPhysics.sPressed == true && carPhysics.wPressed == false && carPhysics.shiftPressed == false){
			em.enabled = true;
		}
		if (carPhysics.shiftPressed == true){
			em.enabled = false;
		}
		if (carPhysics.slide == true && carPhysics.sPressed == false){
			em.enabled = false;
		}
		if (carPhysics.slideB == true && carPhysics.sPressed == false){
			em.enabled = false;
		}
		if (carPhysics.slide == true && carPhysics.sPressed == true){
			em.enabled = true;
		}
		if (carPhysics.slideB == true && carPhysics.sPressed == true){
			em.enabled = true;
		}
	}
}
