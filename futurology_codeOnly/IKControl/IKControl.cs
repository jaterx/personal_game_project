using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class IKControl : MonoBehaviour {

	protected Animator anim;

	public bool ikActive = false;
	public Transform rightHandObj = null;
	public Transform leftHandObj = null;
	public Transform lookObj = null;

	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	//a callback for calculating IK
	void OnAnimatorIK()
	{

		if (NVIDIA.Ansel.IsSessionActive == true) {
			return;		
		}

		if(anim) {

			if(ikActive) {

				if(lookObj != null) {
					anim.SetLookAtWeight(1);
					anim.SetLookAtPosition(lookObj.position);
				}    

				if(rightHandObj != null) {
					anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
					anim.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
					anim.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
					anim.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
				}

				if(leftHandObj != null) {
					anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
					anim.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
					anim.SetIKPosition(AvatarIKGoal.LeftHand,leftHandObj.position);
					anim.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObj.rotation);
				} 

			}
				
			else {          
				anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
				anim.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
				anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
				anim.SetIKRotationWeight(AvatarIKGoal.LeftHand,0); 
				anim.SetLookAtWeight(0);
			}
		}

		if (Input.GetKey(KeyCode.I)){
			ikActive = true;
		}
		if (Input.GetKey(KeyCode.K)){
			ikActive = false;
		}

	}    
}
