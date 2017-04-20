using UnityEngine;
using System.Collections;

public class RhinoAnimator : MonoBehaviour 
{
	Animator anim;
	//int jumpHash = Animator.StringToHash("Jump");
	//int runStateHash = Animator.StringToHash("Base Layer.Run");
	public static float checkVar;
	public static float stunt;

	void Start ()
	{
		anim = GetComponent<Animator>();
	}


	void Update ()
	{
		//float move = Input.GetAxis ("Vertical");
		//anim.SetFloat("Speed", move);
		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		//AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if (MapMagicDemo.CameraController.controlMode == 0){
			if (RhinoPhysics.carSpeed > 0.1f && RhinoPhysics.carSpeed < 7.5f) {
				checkVar = 3.0f;
			}
			if (RhinoPhysics.carSpeed >= 7.5f && RhinoPhysics.carSpeed <= 15.0f){
				checkVar = 5.0f;
			}
			if (RhinoPhysics.carSpeed >= 0.0f && RhinoPhysics.carSpeed <= 0.5f){
				checkVar = 1.0f;
			}

			if (RhinoPhysics.carSpeed < -0.1f && RhinoPhysics.carSpeed > -7.5f) {
				checkVar = 3.0f;
			}
			if (RhinoPhysics.carSpeed <= -7.5f && RhinoPhysics.carSpeed >= -15.0f){
				checkVar = 5.0f;
			}
			if (RhinoPhysics.carSpeed <= 0.0f && RhinoPhysics.carSpeed >= -0.5f){
				checkVar = 1.0f;
			}

		}
		if (MapMagicDemo.CameraController.controlMode == 1){
			if (clickToMove.moveConfirmed == true){
				checkVar = 5.0f;
			}
			if (clickToMove.moveConfirmed == false){
				checkVar = 1.0f;
			}
			if (Input.GetKeyUp (KeyCode.R)) {
				//anim.enabled = true;
				stunt = 3.0f;
			}
			if (!Input.GetKeyUp (KeyCode.R)) {
				//anim.enabled = false;
				stunt = 1.0f;
			}
		}

		anim.SetFloat ("RhinoSpeed", checkVar);
		anim.SetFloat ("Magnified", stunt);
	}
}