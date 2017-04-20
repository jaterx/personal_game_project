using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour {

	public static int unitCount;
	public static int baseCount;
	public static int genCount;
	public static int uavCount;
	public static int rhinoCount;
	public static int sateCount;
	public static float currency;
	public static int qualityCounter;
	public static bool u1Killed;
	public static bool u2Killed;
	public static bool u3Killed;
	public static bool u4Killed;
	public static bool r1Killed;
	public static bool r2Killed;
	public static bool r3Killed;
	public static bool r4Killed;
	public static bool bKilled;
	public static bool g1Killed;
	public static bool g2Killed;
	public static bool sKilled;
	public static GameObject win;
	public static GameObject specialWin;
	public static GameObject lose;
	public static GameObject specialLose;
	public static bool gameWon;
	public static bool gameLost;
	public static bool enemyLost;
	public static GameObject bgInfo;

	// Use this for initialization
	void Start () {
		unitCount = 0;
		baseCount = 0;
		sateCount = 0;
		genCount = 0;
		uavCount = 0;
		rhinoCount = 0;
		currency = 100000;
		qualityCounter = 3;
		bgInfo = GameObject.Find ("WinLossCav");
		win = GameObject.Find ("Win");
		specialWin = GameObject.Find ("SpecialWin");
		lose = GameObject.Find ("Lose");
		specialLose = GameObject.Find ("SpecialLose");
		win.SetActive (false);
		specialWin.SetActive (false);
		lose.SetActive (false);
		specialLose.SetActive (false);
		gameWon = false;
		gameLost = false;
		u1Killed = false;
		u2Killed = false;
		u3Killed = false;
		u4Killed = false;
		r1Killed = false;
		r2Killed = false;
		r3Killed = false;
		r4Killed = false;
		bKilled = false;
		g1Killed = false;
		g2Killed = false;
		sKilled = false;
		enemyLost = false;
		bgInfo.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		unitCount = baseCount + genCount + sateCount + uavCount + rhinoCount;
		currency += sateCount * 50 * Time.deltaTime;

		if (unitCount == 0 && buildControl.gameStarted == true){
			bgInfo.SetActive (true);
			win.SetActive (false);
			lose.SetActive (true);
			specialWin.SetActive (false);
			specialLose.SetActive (false);
			gameLost = true;
			//this.gameObject.SetActive (false);
		}

		if (u1Killed == true && u2Killed == true && u3Killed == true && u4Killed == true && r1Killed == true && r2Killed == true && r3Killed == true && r4Killed == true && sKilled == true && bKilled == true && g1Killed == true && g2Killed == true) {
			bgInfo.SetActive (true);
			win.SetActive (true);
			lose.SetActive (false);
			specialWin.SetActive (false);
			specialLose.SetActive (false);
			gameWon = true;
			//this.gameObject.SetActive (false);
		}

		if(unitCount >= 50 && buildControl.gameStarted == true){
			bgInfo.SetActive (true);
			win.SetActive (false);
			lose.SetActive (false);
			specialWin.SetActive (true);
			specialLose.SetActive (false);
			enemyLost = true;
			//this.gameObject.SetActive (false);
		}
		if(currency < 7000 && buildControl.sateAlreadyBuilt == false){
			bgInfo.SetActive (true);
			win.SetActive (false);
			lose.SetActive (false);
			specialWin.SetActive (false);
			specialLose.SetActive (true);
			//this.gameObject.SetActive (false);
		}

	}
}
