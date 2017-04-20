using UnityEngine;
using System.Collections;

public class UAVShotBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		transform.position += transform.forward * Time.deltaTime * 100f;
	}
}
