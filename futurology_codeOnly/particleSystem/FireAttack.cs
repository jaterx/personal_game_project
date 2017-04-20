using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour {
	ParticleSystem ps;
	ParticleSystem.EmissionModule em;
	public GameObject rhinoFound;
	public static bool fireSpelled;
	public float rhinoAttackDistance;
	public static bool manuallyAttack;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
		em = ps.emission;
		em.enabled = false;
		//rhinoFound = GameObject.FindGameObjectWithTag ("Rhino");
		fireSpelled = false;
		rhinoAttackDistance = 30.0f;
		manuallyAttack = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		//first person view rhino attack
		if (MapMagicDemo.CameraController.controlMode == 0){
			if (RideRhino.rhinoDrive == true){
				if (Input.GetMouseButton(0)){
					manuallyAttack = true;
					em.enabled = true;
					fireSpelled = true;
				}
				if (!Input.GetMouseButton(0)){
					manuallyAttack = false;
					em.enabled = false;
					fireSpelled = false;
				}
			}
		}

		//isometric view rhino attack
		if (MapMagicDemo.CameraController.controlMode == 1) {
			if (buildControl.rhinoAlreadyBuilt == true){
				if (Input.GetKey(KeyCode.B)){
					manuallyAttack = true;
					em.enabled = true;
					fireSpelled = true;
				}
				if (Input.GetKey(KeyCode.N)){
					manuallyAttack = false;
					em.enabled = false;
					fireSpelled = false;
				}
			}
		}

		//semi-automatic rhino attack
		float distanceRR1 = Vector3.Distance (this.transform.position, NavRhinoE.getThis.transform.position);
		float distanceRR2 = Vector3.Distance (this.transform.position, NavRhinoE2.getThis.transform.position);
		float distanceRR3 = Vector3.Distance (this.transform.position, NavRhinoE3.getThis.transform.position);
		float distanceRR4 = Vector3.Distance (this.transform.position, NavRhinoE4.getThis.transform.position);
		float distanceRU1 = Vector3.Distance (this.transform.position, NavEU.getThis.transform.position);
		float distanceRU2 = Vector3.Distance (this.transform.position, NavEU2.getThis.transform.position);
		float distanceRU3 = Vector3.Distance (this.transform.position, NavEU3.getThis.transform.position);
		float distanceRU4 = Vector3.Distance (this.transform.position, NavEU4.getThis.transform.position);
		if (distanceRR1 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRR2 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRR3 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRR4 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRU1 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRU2 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRU3 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRU4 <= rhinoAttackDistance){
			em.enabled = true;
			fireSpelled = true;
		}
		if (distanceRR1 > rhinoAttackDistance && distanceRR2 > rhinoAttackDistance && distanceRR3 > rhinoAttackDistance && distanceRR4 > rhinoAttackDistance && distanceRU1 > rhinoAttackDistance && distanceRU2 > rhinoAttackDistance && distanceRR2 > rhinoAttackDistance && distanceRU3 > rhinoAttackDistance){
			if (manuallyAttack == false) {
				em.enabled = false;
				fireSpelled = false;
			}
		}
	}
}
