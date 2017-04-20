using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavE : MonoBehaviour {

	public NavMeshAgent agent;
	//public GameObject friendlyUAV;
	//public GameObject friendlyRhino;
	//public GameObject friendlyBase;
	//public GameObject friendlySate;
	//public GameObject friendlyGen;
	public static bool targetLock;
	public float distanceUAV = 99999.9f;
	public float distanceRhino = 99999.9f;
	public float distanceBase = 99999.9f;
	public float distanceSate = 99999.9f;
	public float distanceGen = 99999.9f;
	public static float threatDistance;
	public static float parkingOffset;
	public static float attackingRange;

	public static Vector3 uav1AnchorPos;
	public static Vector3 rhino1AnchorPos;
	public static Vector3 uav2AnchorPos;
	public static Vector3 rhino2AnchorPos;
	public static Vector3 uav3AnchorPos;
	public static Vector3 rhino3AnchorPos;
	public static Vector3 uav4AnchorPos;
	public static Vector3 rhino4AnchorPos;

	public static float running;
	public static float attacking;
	public static float hitting;
	public static float dying;

	public static float running2;
	public static float attacking2;
	public static float hitting2;
	public static float dying2;

	public static float running3;
	public static float attacking3;
	public static float hitting3;
	public static float dying3;

	public static float running4;
	public static float attacking4;
	public static float hitting4;
	public static float dying4;

	Animator anim1;
	Animator anim2;
	Animator anim3;
	Animator anim4;

	public static float returned;

	// Use this for initialization
	void Start () {
		threatDistance = 150.0f;
		parkingOffset = 10.0f;
		attackingRange = 11.0f;
		targetLock = false;
		agent = this.GetComponent<NavMeshAgent> ();

		//uav1AnchorPos = GameObject.Find ("UAVE").transform.position;
		//uav2AnchorPos = GameObject.Find ("UAVE2").transform.position;
		//uav3AnchorPos = GameObject.Find ("UAVE3").transform.position;
		//uav4AnchorPos = GameObject.Find ("UAVE4").transform.position;
		rhino1AnchorPos = GameObject.Find ("RhinoE").transform.position;
		rhino2AnchorPos = GameObject.Find ("RhinoE2").transform.position;
		rhino3AnchorPos = GameObject.Find ("RhinoE3").transform.position;
		rhino4AnchorPos = GameObject.Find ("RhinoE4").transform.position;

		anim1 = GameObject.Find("RhinoE").GetComponent<Animator> ();
		anim2 = GameObject.Find("RhinoE2").GetComponent<Animator> ();
		anim3 = GameObject.Find("RhinoE3").GetComponent<Animator> ();
		anim4 = GameObject.Find("RhinoE4").GetComponent<Animator> ();
		running = 2.0f;
		dying = 2.0f;
		hitting = 2.0f;
		attacking = 2.0f;
		running2 = 2.0f;
		dying2 = 2.0f;
		hitting2 = 2.0f;
		attacking2 = 2.0f;
		running3 = 2.0f;
		dying3 = 2.0f;
		hitting3 = 2.0f;
		attacking3 = 2.0f;
		running4 = 2.0f;
		dying4 = 2.0f;
		hitting4 = 2.0f;
		attacking4 = 2.0f;

	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		//if (dying != 3.0f) {
			if (buildControl.baseAlreadyBuilt == true) {
				foreach (GameObject friendlyBase in GameObject.FindGameObjectsWithTag ("Base")) {
					distanceBase = Vector3.Distance (this.transform.position, friendlyBase.transform.position);
					if (distanceBase <= threatDistance) {
						targetLock = true;
						if (this.name == "RhinoE") {
							running = 3.0f;
						}
						if (this.name == "RhinoE2") {
							running2 = 3.0f;
						}
						if (this.name == "RhinoE3") {
							running3 = 3.0f;
						}
						if (this.name == "RhinoE4") {
							running4 = 3.0f;
						}
						Vector3 targetBasePos = new Vector3 (friendlyBase.transform.position.x, friendlyBase.transform.position.y, friendlyBase.transform.position.z + parkingOffset);
						agent.SetDestination (targetBasePos);
						if (distanceBase <= attackingRange) {
							this.transform.LookAt (friendlyBase.transform);
							if (this.name == "RhinoE") {
								running = 1.0f;
								attacking = 3.0f;
							}
							if (this.name == "RhinoE2") {
								running2 = 1.0f;
								attacking2 = 3.0f;
							}
							if (this.name == "RhinoE3") {
								running3 = 1.0f;
								attacking3 = 3.0f;
							}
							if (this.name == "RhinoE4") {
								running4 = 1.0f;
								attacking4 = 3.0f;
							}
						}
					}
					if (distanceBase >= threatDistance) {
						targetLock = false;
					}
				}
			}

			if (buildControl.genAlreadyBuilt == true) {
				foreach (GameObject friendlyGen in GameObject.FindGameObjectsWithTag ("Generator")) {
					distanceGen = Vector3.Distance (this.transform.position, friendlyGen.transform.position);
					if (distanceGen <= threatDistance) {
						targetLock = true;
						if (this.name == "RhinoE") {
							running = 3.0f;
						}
						if (this.name == "RhinoE2") {
							running2 = 3.0f;
						}
						if (this.name == "RhinoE3") {
							running3 = 3.0f;
						}
						if (this.name == "RhinoE4") {
							running4 = 3.0f;
						}
						Vector3 targetGenPos = new Vector3 (friendlyGen.transform.position.x, friendlyGen.transform.position.y, friendlyGen.transform.position.z + parkingOffset);
						agent.SetDestination (targetGenPos);
						if (distanceBase <= attackingRange) {
							this.transform.LookAt (friendlyGen.transform);
							if (this.name == "RhinoE") {
								running = 1.0f;
								attacking = 3.0f;
							}
							if (this.name == "RhinoE2") {
								running2 = 1.0f;
								attacking2 = 3.0f;
							}
							if (this.name == "RhinoE3") {
								running3 = 1.0f;
								attacking3 = 3.0f;
							}
							if (this.name == "RhinoE4") {
								running4 = 1.0f;
								attacking4 = 3.0f;
							}
						}
					}
					if (distanceBase >= threatDistance) {
						targetLock = false;
					}
				}
			}

			if (buildControl.sateAlreadyBuilt == true) {
				foreach (GameObject friendlySate in GameObject.FindGameObjectsWithTag ("Dish")) {
					distanceSate = Vector3.Distance (this.transform.position, friendlySate.transform.position);
					if (distanceSate <= threatDistance) {
						targetLock = true;
						if (this.name == "RhinoE") {
							running = 3.0f;
						}
						if (this.name == "RhinoE2") {
							running2 = 3.0f;
						}
						if (this.name == "RhinoE3") {
							running3 = 3.0f;
						}
						if (this.name == "RhinoE4") {
							running4 = 3.0f;
						}
						Vector3 targetSatePos = new Vector3 (friendlySate.transform.position.x, friendlySate.transform.position.y, friendlySate.transform.position.z + parkingOffset);
						agent.SetDestination (targetSatePos);
						if (distanceBase <= attackingRange) {
							this.transform.LookAt (friendlySate.transform);
							if (this.name == "RhinoE") {
								running = 1.0f;
								attacking = 3.0f;
							}
							if (this.name == "RhinoE2") {
								running2 = 1.0f;
								attacking2 = 3.0f;
							}
							if (this.name == "RhinoE3") {
								running3 = 1.0f;
								attacking3 = 3.0f;
							}
							if (this.name == "RhinoE4") {
								running4 = 1.0f;
								attacking4 = 3.0f;
							}
						}
					}
					if (distanceBase >= threatDistance) {
						targetLock = false;
					}
				}
			}

			if (buildControl.rhinoAlreadyBuilt == true) {
				foreach (GameObject friendlyRhino in GameObject.FindGameObjectsWithTag ("Rhino")) {
					distanceRhino = Vector3.Distance (this.transform.position, friendlyRhino.transform.position);
					if (distanceRhino <= threatDistance) {
						targetLock = true;
						if (this.name == "RhinoE") {
							running = 3.0f;
						}
						if (this.name == "RhinoE2") {
							running2 = 3.0f;
						}
						if (this.name == "RhinoE3") {
							running3 = 3.0f;
						}
						if (this.name == "RhinoE4") {
							running4 = 3.0f;
						}
						Vector3 targetRhinoPos = new Vector3 (friendlyRhino.transform.position.x, friendlyRhino.transform.position.y, friendlyRhino.transform.position.z + parkingOffset);
						agent.SetDestination (targetRhinoPos);
						if (distanceBase <= attackingRange) {
							this.transform.LookAt (friendlyRhino.transform);
							if (this.name == "RhinoE") {
								running = 1.0f;
								attacking = 3.0f;
							}
							if (this.name == "RhinoE2") {
								running2 = 1.0f;
								attacking2 = 3.0f;
							}
							if (this.name == "RhinoE3") {
								running3 = 1.0f;
								attacking3 = 3.0f;
							}
							if (this.name == "RhinoE4") {
								running4 = 1.0f;
								attacking4 = 3.0f;
							}
						}
					}
					if (distanceBase >= threatDistance) {
						targetLock = false;
					}
				}
			}

			if (buildControl.uavAlreadyBuilt == true) {
				foreach (GameObject friendlyUAV in GameObject.FindGameObjectsWithTag ("UAV")) {
					distanceUAV = Vector3.Distance (this.transform.position, friendlyUAV.transform.position);
					if (distanceUAV <= threatDistance) {
						targetLock = true;
						if (this.name == "RhinoE") {
							running = 3.0f;
						}
						if (this.name == "RhinoE2") {
							running2 = 3.0f;
						}
						if (this.name == "RhinoE3") {
							running3 = 3.0f;
						}
						if (this.name == "RhinoE4") {
							running4 = 3.0f;
						}
						Vector3 targetUAVPos = new Vector3 (friendlyUAV.transform.position.x, friendlyUAV.transform.position.y, friendlyUAV.transform.position.z + parkingOffset);
						agent.SetDestination (targetUAVPos);
						if (distanceBase <= attackingRange) {
							this.transform.LookAt (friendlyUAV.transform);
							if (this.name == "RhinoE") {
								running = 1.0f;
								attacking = 3.0f;
							}
							if (this.name == "RhinoE2") {
								running2 = 1.0f;
								attacking2 = 3.0f;
							}
							if (this.name == "RhinoE3") {
								running3 = 1.0f;
								attacking3 = 3.0f;
							}
							if (this.name == "RhinoE4") {
								running4 = 1.0f;
								attacking4 = 3.0f;
							}
						}
					}
					if (distanceBase >= threatDistance) {
						targetLock = false;
					}
				}
			}
		//}

		//first rhino
		if (this.name == "RhinoE"){
			if (HPRhinoE.damaged == true){
				if (dying != 3.0f){
					hitting = 3.0f;
				}
			}

			if (HPRhinoE.damaged == false){
				if (targetLock == true){
					if (dying != 3.0f){
						running = 3.0f;
						hitting = 1.0f;
					}
				}
				if (targetLock == false){
					if (dying != 3.0f){
						running = 1.0f;
						//hitting = 3.0f;
					}
				}

			}

			if (HPRhinoE.damageDealing <= 0.0f){
				hitting = 1.0f;
				running = 1.0f;
				attacking = 1.0f;
				dying = 3.0f;
				agent.Stop ();
				agent.enabled = false;
			}

			anim1.SetFloat ("Running", running);
			anim1.SetFloat ("Attacking", attacking);
			anim1.SetFloat ("Dead", dying);
			anim1.SetFloat ("Damaged", hitting);
		}

		//second rhino
		if (this.name == "RhinoE2"){
			if (HPRhinoE2.damaged == true){
				if (dying2 != 3.0f){
					hitting2 = 3.0f;
				}
			}

			if (HPRhinoE2.damaged == false){
				if (targetLock == true){
					if (dying2 != 3.0f){
						running2 = 3.0f;
						hitting2 = 1.0f;
					}
				}
				if (targetLock == false){
					if (dying2 != 3.0f){
						running2 = 1.0f;
						//hitting2 = 3.0f;
					}
				}

			}

			if (HPRhinoE2.damageDealing <= 0.0f){
				hitting2 = 1.0f;
				running2 = 1.0f;
				attacking2 = 1.0f;
				dying2 = 3.0f;
				agent.Stop ();
				agent.enabled = false;
			}
			anim2.SetFloat ("Running", running2);
			anim2.SetFloat ("Attacking", attacking2);
			anim2.SetFloat ("Dead", dying2);
			anim2.SetFloat ("Damaged", hitting2);
		}

		//third rhino
		if (this.name == "RhinoE3"){
			if (HPRhinoE3.damaged == true){
				if (dying3 != 3.0f){
					hitting3 = 3.0f;
				}
			}

			if (HPRhinoE3.damaged == false){
				if (targetLock == true){
					if (dying3 != 3.0f){
						running3 = 3.0f;
						hitting3 = 1.0f;
					}
				}
				if (targetLock == false){
					if (dying3 != 3.0f){
						running3 = 1.0f;
						//hitting3 = 3.0f;
					}
				}

			}

			if (HPRhinoE3.damageDealing <= 0.0f){
				hitting3 = 1.0f;
				running3 = 1.0f;
				attacking3 = 1.0f;
				dying3 = 3.0f;
				agent.Stop ();
				agent.enabled = false;
			}
			anim3.SetFloat ("Running", running3);
			anim3.SetFloat ("Attacking", attacking3);
			anim3.SetFloat ("Dead", dying3);
			anim3.SetFloat ("Damaged", hitting3);
		}

		//fourth rhino
		if (this.name == "RhinoE4"){
			if (HPRhinoE4.damaged == true){
				if (dying4 != 3.0f){
					hitting4 = 3.0f;
				}
			}

			if (HPRhinoE4.damaged == false){
				if (targetLock == true){
					if (dying4 != 3.0f){
						running4 = 3.0f;
						hitting4 = 1.0f;
					}
				}
				if (targetLock == false){
					if (dying4 != 3.0f){
						running4 = 1.0f;
						//hitting4 = 3.0f;
					}
				}

			}

			if (HPRhinoE4.damageDealing <= 0.0f){
				hitting4 = 1.0f;
				running4 = 1.0f;
				attacking4 = 1.0f;
				dying4 = 3.0f;
				agent.Stop ();
				agent.enabled = false;
			}
			anim4.SetFloat ("Running", running4);
			anim4.SetFloat ("Attacking", attacking4);
			anim4.SetFloat ("Dead", dying4);
			anim4.SetFloat ("Damaged", hitting4);
		}

	}
}
