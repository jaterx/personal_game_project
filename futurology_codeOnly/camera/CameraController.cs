using UnityEngine;
using System.Collections;

//using plugins to link with Char Controller provided by terrain engine

using MapMagic;

namespace MapMagicDemo
{
	public class CameraController : MonoBehaviour 
	{
		public Camera cam;
		public Transform hero;
		
		public bool movable;
		public float velocity = 4;
		public float follow = 0.1f;
		
		private Vector3 pivot = new Vector3(0,0,0);
		
		public int rotateMouseButton = 0;
		public bool lockCursor = true; //no mouse 1 reqired
		public float elevation = 1.5f;
		public float sensitivity = 1f;

		public float rotationX = 0;
		public float rotationY = 190;
		 
		public static int driveMode;

		private Vector3 oldPos;

		public static int controlMode = 0;

		public static GameObject accessChar;
	
		public void Start ()
		{
			if (cam==null) cam = Camera.main;
			pivot = cam.transform.position;
			accessChar = this.gameObject;
			driveMode = 0;
		}

		public void LateUpdate () //updating after hero is moved and all other scene changes made
		{
			if (NVIDIA.Ansel.IsSessionActive == true) {
				return;		
			}
			//locking cursor
			if (lockCursor)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			
			//reading controls
			if (controlMode == 0){
				if (driveMode == 0) {
					rotationY += Input.GetAxis("Mouse X")*sensitivity;
					rotationX -= Input.GetAxis("Mouse Y")*sensitivity;
				}if (driveMode == 1) {
					rotationY = 0;
					rotationX = 0;
				}
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			if (Input.GetKey(KeyCode.C)) {
				controlMode = 0;
				lockCursor = true;

			}
			if (Input.GetKey(KeyCode.V)) {
				controlMode = 1;
				lockCursor = false;
			}

            //setting cam
            if (hero!=null) pivot = hero.position + new Vector3(0, elevation, 0);
			
			//moving
			if (follow > 0.000001f)
			{
				Vector3 moveVector = cam.transform.position - oldPos;
				float moveRotationY = moveVector.Angle();
				float delta = Mathf.DeltaAngle(rotationY, moveRotationY);
				
				if (Mathf.Abs(delta) > follow*Time.deltaTime) rotationY += (delta>0? 1 : -1) * follow * Time.deltaTime;
				else rotationY = moveRotationY;
			}
			oldPos = cam.transform.position;
			
			cam.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
			cam.transform.position = pivot;
		}
	}
}
