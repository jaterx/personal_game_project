using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyControl : MonoBehaviour {
	
	public static Text currencyFTxt;

	// Use this for initialization
	void Start () {
		currencyFTxt = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}
		currencyFTxt.text = ((int)GlobalControl.currency).ToString ();
	}
}
