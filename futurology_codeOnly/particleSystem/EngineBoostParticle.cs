using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBoostParticle : MonoBehaviour {

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
		if (carPhysics.shiftPressed == true){
			em.enabled = true;
		}
		if (carPhysics.shiftPressed == false){
			em.enabled = false;
		}
	}
}
