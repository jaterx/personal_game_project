using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavRhinoE2 : MonoBehaviour {

	public NavMeshAgent agent;
	//public GameObject friendlyUAV;
	//public GameObject friendlyRhino;
	//public GameObject friendlyBase;
	//public GameObject friendlySate;
	//public GameObject friendlyGen;
	public static bool targetLock;
	public static float distanceUAV = 99999.9f;
	public static float distanceRhino = 99999.9f;
	public float distanceBase = 99999.9f;
	public float distanceSate = 99999.9f;
	public float distanceGen = 99999.9f;
	public static float threatDistance;
	public static float parkingOffset;
	public static float attackingRange;

	public static Vector3 rhino2AnchorPos;

	public static float running;
	public static float attacking;
	public static float hitting;
	public static float dying;

	public static GameObject getThis;
	public static bool retreat;
	public static bool retreatLocked;
	Animator anim;

	// Use this for initialization
	void Start () {
		threatDistance = 150.0f;
		parkingOffset = 15.0f;
		attackingRange = 24.0f;
		targetLock = false;
		agent = this.GetComponent<NavMeshAgent> ();
		retreat = false;
		retreatLocked = false;
		//uav1AnchorPos = GameObject.FindGameObjectWithTag ("UAVE").transform.position;
		//uav2AnchorPos = GameObject.FindGameObjectWithTag ("UAVE2").transform.position;
		//uav3AnchorPos = GameObject.FindGameObjectWithTag ("UAVE3").transform.position;
		//uav4AnchorPos = GameObject.FindGameObjectWithTag ("UAVE4").transform.position;
		rhino2AnchorPos = this.transform.position;
		//rhino2AnchorPos = GameObject.FindGameObjectWithTag ("RhinoE2").transform.position;
		//rhino3AnchorPos = GameObject.FindGameObjectWithTag ("RhinoE3").transform.position;
		//rhino4AnchorPos = GameObject.FindGameObjectWithTag ("RhinoE4").transform.position;

		anim = this.GetComponent<Animator> ();
		running = 2.0f;
		dying = 2.0f;
		attacking = 2.0f;
		hitting = 2.0f;
		getThis = this.gameObject;
	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		//Debug.Log (distanceUAV);

		if (buildControl.baseAlreadyBuilt == true){
			foreach (GameObject friendlyBase in GameObject.FindGameObjectsWithTag ("Base")){
				distanceBase = Vector3.Distance (this.transform.position, friendlyBase.transform.position);
				if (distanceBase <= threatDistance){
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
					targetLock = true;
					if (distanceBase >= attackingRange) {
						running = 3.0f;
						attacking = 1.0f;
					}
					Vector3 targetBasePos = new Vector3 (friendlyBase.transform.position.x, friendlyBase.transform.position.y, friendlyBase.transform.position.z - parkingOffset);
					agent.SetDestination (targetBasePos);
					if (distanceBase <= attackingRange){
						if (dying == 1.0f) {
							this.transform.LookAt (friendlyBase.transform);
						}
						running = 1.0f;
						attacking = 3.0f;
					}
				}
			}
		}

		if (buildControl.genAlreadyBuilt == true){
			foreach (GameObject friendlyGen in GameObject.FindGameObjectsWithTag ("Generator")) {
				distanceGen = Vector3.Distance (this.transform.position, friendlyGen.transform.position);
				if (distanceGen <= threatDistance) {
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
					targetLock = true;
					if (distanceGen >= attackingRange) {
						running = 3.0f;
						attacking = 1.0f;
					}
					Vector3 targetGenPos = new Vector3 (friendlyGen.transform.position.x, friendlyGen.transform.position.y, friendlyGen.transform.position.z - parkingOffset);
					agent.SetDestination (targetGenPos);
					if (distanceGen <= attackingRange){
						if (dying == 1.0f) {
							this.transform.LookAt (friendlyGen.transform);
						}
						running = 1.0f;
						attacking = 3.0f;
					}
				}
				if (distanceGen >= threatDistance) {
					targetLock = false;
				}
			}
		}

		if (buildControl.sateAlreadyBuilt == true){
			foreach (GameObject friendlySate in GameObject.FindGameObjectsWithTag ("Dish")) {
				distanceSate = Vector3.Distance (this.transform.position, friendlySate.transform.position);
				if (distanceSate <= threatDistance) {
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
					targetLock = true;
					if (distanceSate >= attackingRange) {
						running = 3.0f;
						attacking = 1.0f;
					}
					Vector3 targetSatePos = new Vector3 (friendlySate.transform.position.x, friendlySate.transform.position.y, friendlySate.transform.position.z - parkingOffset);
					agent.SetDestination (targetSatePos);
					if (distanceSate <= attackingRange){
						if (dying == 1.0f) {
							this.transform.LookAt (friendlySate.transform);
						}
						running = 1.0f;
						attacking = 3.0f;
					}
				}
				if (distanceSate >= threatDistance) {
					targetLock = false;
				}
			}
		}

		if (buildControl.rhinoAlreadyBuilt == true){
			foreach (GameObject friendlyRhino in GameObject.FindGameObjectsWithTag ("Rhino")) {
				distanceRhino = Vector3.Distance (this.transform.position, friendlyRhino.transform.position);
				if (distanceRhino <= threatDistance) {
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
					targetLock = true;
					retreat = true;
					if (distanceBase >= attackingRange) {
						running = 3.0f;
						attacking = 1.0f;
					}
					Vector3 targetRhinoPos = new Vector3 (friendlyRhino.transform.position.x, friendlyRhino.transform.position.y, friendlyRhino.transform.position.z - parkingOffset);
					agent.SetDestination (targetRhinoPos);
					if (distanceRhino <= attackingRange){
						if (dying == 1.0f) {
							this.transform.LookAt (friendlyRhino.transform);
						}
						running = 1.0f;
						attacking = 3.0f;
					}
				}
				if (distanceRhino >= threatDistance) {
					targetLock = false;
				}
			}
		}

		if (buildControl.uavAlreadyBuilt == true){
			foreach (GameObject friendlyUAV in GameObject.FindGameObjectsWithTag ("UAV")){
				distanceUAV = Vector3.Distance (this.transform.position, friendlyUAV.transform.position);
				if (distanceUAV <= threatDistance){
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
					targetLock = true;
					retreat = true;
					if (distanceBase >= attackingRange) {
						running = 3.0f;
						attacking = 1.0f;
					}
					Vector3 targetUAVPos = new Vector3 (friendlyUAV.transform.position.x, friendlyUAV.transform.position.y, friendlyUAV.transform.position.z - parkingOffset);
					agent.SetDestination (targetUAVPos);
					if (distanceUAV <= attackingRange){
						if (dying == 1.0f) {
							this.transform.LookAt (friendlyUAV.transform);
						}
						running = 1.0f;
						attacking = 3.0f;
					}
				}
				if (distanceUAV >= threatDistance) {
					targetLock = false;
				}
			}
		}

		if (retreat == true && (GameObject.Find("UAV") == null) && (GameObject.Find("Rhino") == null)){
			retreatLocked = true;
		}

		if (distanceRhino > threatDistance && distanceUAV > threatDistance && retreat == true){
			retreatLocked = true;
		}

		if (distanceRhino > threatDistance && (GameObject.Find("UAV") == null) && retreat == true){
			retreatLocked = true;
		}

		if (distanceUAV > threatDistance && (GameObject.Find("Rhino") == null) && retreat == true){
			retreatLocked = true;
		}

		if (retreatLocked == true){
			//agent.SetDestination (uav1AnchorPos);
			agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
			agent.SetDestination(rhino2AnchorPos);
			running = 3.0f;
			attacking = 1.0f;
			//float distanceBack = Vector3.Distance (this.transform.position, uav1AnchorPos);
			retreat = false;
			retreatLocked = false;
		}

		if (HPRhinoE2.damaged == true){
			hitting = 3.0f;
		}
		if (HPRhinoE2.damaged == false){
			hitting = 1.0f;
		}

		//Debug.Log (HPRhinoE.damageDealing);

		if (HPRhinoE2.damageDealing <= 1.0f){
			hitting = 1.0f;
			running = 1.0f;
			attacking = 1.0f;
			dying = 3.0f;
			agent.Stop ();
		}

		anim.SetFloat ("Running", running);
		anim.SetFloat ("Attacking", attacking);
		anim.SetFloat ("Dead", dying);
		anim.SetFloat ("Damaged", hitting);
	}
}
