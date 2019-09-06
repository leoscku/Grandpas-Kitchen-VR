using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public GameObject player;
	public Transform hand;
	public Transform trackingSpace; 
	public GameObject cane; 
	private GameObject curCane; 
	private Vector3 controllerPosition;
	private Vector3 controllerRotation;
	
	private bool moveLock = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		handleMovementAlter();
	}
	
	
	void handleMovement() {
		
		if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0.2){
			if (!moveLock){
				
				controllerPosition = hand.localPosition;
				controllerRotation = hand.localEulerAngles;

				//Debug.Log("INITIAL LOCATION: " + controllerPosition.x + ", " + controllerPosition.y + ", " + controllerPosition.z);
				//pointerLine.GetComponent<LineRenderer>().enabled = false;
				moveLock = true;
			} else {
				float speed = 0.5f;
				Vector3 velocity = (hand.localPosition - controllerPosition) * speed;
				//float angle = Quaternion.Angle(controllerRotation, hand.rotation) * 0.01f;    
				//Quaternion difference = controllerRotation * Quaternion.Inverse(hand.rotation);
				Vector3 angle = hand.localEulerAngles - controllerRotation;
				float angleY = angle.y;
				Debug.Log(angle.x + ", " + angle.y + ", " + angle.z);
				
				if (angleY <= -180)
				{
					angleY += 360;
				} else if (angleY >= 180)
				{
					angleY -= 360;
				}
				
				angleY *= 0.01f;

                //Debug.Log(hand.localPosition.x + ", " + hand.localPosition.y + ", " + hand.localPosition.z);
                //Debug.Log(angleY);

                CharacterController controller = player.GetComponent<CharacterController>();

                Vector3 dir = transform.TransformDirection(new Vector3(velocity.x, 0, velocity.z));
                float movSpeed = 40.0f;

                controller.SimpleMove(dir * movSpeed);

                //player.transform.Translate(new Vector3(velocity.x, 0, velocity.z), player.transform);
                player.transform.Rotate(0, angleY, 0);
			}
                
        } else {
			moveLock = false;
		}
		


	}
	
	void handleMovementAlter() {
		if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.2){
			if (!moveLock){
				controllerPosition = trackingSpace.worldToLocalMatrix.MultiplyPoint3x4(hand.position); 
				moveLock = true;
				curCane = Instantiate(cane, new Vector3(hand.transform.position.x, hand.transform.position.y-0.55f, hand.transform.position.z), Quaternion.Euler(0, 90, 0) * player.transform.rotation);
				curCane.transform.SetParent(player.transform); 
			} else {
				curCane.transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y-0.55f, hand.transform.position.z); 
				Vector3 velocity = - (trackingSpace.worldToLocalMatrix.MultiplyPoint3x4(hand.position) - controllerPosition);

                CharacterController controller = player.GetComponent<CharacterController>();

                Vector3 dir = transform.TransformDirection(new Vector3(velocity.x, 0, velocity.z));
				
                float movSpeed = 200.0f;
				//Debug.Log(dir * movSpeed); 

                controller.SimpleMove(velocity * movSpeed);

				
				controllerPosition = trackingSpace.worldToLocalMatrix.MultiplyPoint3x4(hand.position); 
				
			}
                
        } else {
			Destroy(curCane); 
			moveLock = false;
		}
	}
	
}
