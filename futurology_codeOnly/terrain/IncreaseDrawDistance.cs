using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDrawDistance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Terrain.activeTerrain.detailObjectDistance = 700;
		Terrain.activeTerrain.detailObjectDensity = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
