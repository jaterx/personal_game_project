using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavEU3 : MonoBehaviour {

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

	public static Vector3 uav3AnchorPos;

	public static GameObject getThis;

	public static bool retreat;
	public static bool retreatLocked;

	Animation anim;

	// Use this for initialization
	void Start () {
		threatDistance = 150.0f;
		parkingOffset = 15.0f;
		attackingRange = 24.0f;
		targetLock = false;
		agent = this.GetComponent<NavMeshAgent> ();
		getThis = this.gameObject;
		retreat = false;
		retreatLocked = false;
		//uav1AnchorPos = GameObject.FindGameObjectWithTag ("UAVE").transform.position;
		//uav2AnchorPos = GameObject.FindGameObjectWithTag ("UAVE2").transform.position;
		uav3AnchorPos = this.transform.position;
		//uav4AnchorPos = GameObject.FindGameObjectWithTag ("UAVE4").transform.position;
		//rhino1AnchorPos = GameObject.FindGameObjectWithTag ("RhinoE").transform.position;
		//rhino2AnchorPos = GameObject.FindGameObjectWithTag ("RhinoE2").transform.position;
		//rhino3AnchorPos = GameObject.FindGameObjectWithTag ("RhinoE3").transform.position;
		//rhino4AnchorPos = GameObject.FindGameObjectWithTag ("RhinoE4").transform.position;

	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if (buildControl.baseAlreadyBuilt == true){
			foreach (GameObject friendlyBase in GameObject.FindGameObjectsWithTag ("Base")){
				distanceBase = Vector3.Distance (this.transform.position, friendlyBase.transform.position);
				if (distanceBase <= threatDistance){
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
					targetLock = true;
					Vector3 targetBasePos = new Vector3 (friendlyBase.transform.position.x, friendlyBase.transform.position.y, friendlyBase.transform.position.z + parkingOffset);
					agent.SetDestination (targetBasePos);
					if (distanceBase <= attackingRange){
						this.transform.LookAt (friendlyBase.transform);
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
					Vector3 targetGenPos = new Vector3 (friendlyGen.transform.position.x, friendlyGen.transform.position.y, friendlyGen.transform.position.z + parkingOffset);
					agent.SetDestination (targetGenPos);
					if (distanceBase <= attackingRange){
						this.transform.LookAt (friendlyGen.transform);
					}
				}
			}
		}

		if (buildControl.sateAlreadyBuilt == true){
			foreach (GameObject friendlySate in GameObject.FindGameObjectsWithTag ("Dish")) {
				distanceSate = Vector3.Distance (this.transform.position, friendlySate.transform.position);
				if (distanceSate <= threatDistance) {
					agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
					targetLock = true;
					Vector3 targetSatePos = new Vector3 (friendlySate.transform.position.x, friendlySate.transform.position.y, friendlySate.transform.position.z + parkingOffset);
					agent.SetDestination (targetSatePos);
					if (distanceBase <= attackingRange){
						this.transform.LookAt (friendlySate.transform);
					}
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
					Vector3 targetRhinoPos = new Vector3 (friendlyRhino.transform.position.x, friendlyRhino.transform.position.y, friendlyRhino.transform.position.z + parkingOffset);
					agent.SetDestination (targetRhinoPos);
					if (distanceBase <= attackingRange){
						this.transform.LookAt (friendlyRhino.transform);
					}
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
					Vector3 targetUAVPos = new Vector3 (friendlyUAV.transform.position.x, friendlyUAV.transform.position.y, friendlyUAV.transform.position.z + parkingOffset);
					agent.SetDestination (targetUAVPos);
					if (distanceBase <= attackingRange){
						this.transform.LookAt (friendlyUAV.transform);
					}
				}
			}
		}

		if (retreat == true && (GameObject.Find("UAV") == null) && (GameObject.Find("Rhino") == null)){
			retreatLocked = true;
		}

		if (distanceUAV > threatDistance && distanceRhino > threatDistance && retreat == true){
			retreatLocked = true;
		}

		if (distanceRhino > threatDistance && (GameObject.Find("UAV") == null) && retreat == true){
			retreatLocked = true;
		}

		if (distanceUAV > threatDistance && (GameObject.Find("Rhino") == null) && retreat == true){
			retreatLocked = true;
		}

		if (retreatLocked == true){
			agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
			//agent.SetDestination (uav1AnchorPos);
			agent.SetDestination (uav3AnchorPos);
			//float distanceBack = Vector3.Distance (this.transform.position, uav1AnchorPos);
			retreat = false;
			retreatLocked = false;
		}
		if (HPUAVE3.damageDealing <= 1.0f) {
			agent.Stop ();
		}
	}
}
