using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpVector : MonoBehaviour {
	
	Vector3 position; 

	// Use this for initialization
	void Start () {
		position = new Vector3(-9.61f, 0.85f, -1.83f); 
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation(transform.position - position, new Vector3(0, 1, 0)) * Quaternion.Euler(-90, 0, 0); 
	}
	
	public void updateDirection(Vector3 pos) {
		position = pos; 
	}
}
