using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHandler : MonoBehaviour {

    public GameHandler handler;
	public OVRGrabber other;

    private OVRGrabber grabber;
    private List<string> rawFoods = new List<string>();


	// Use this for initialization
	void Start () {
        grabber = this.GetComponent<OVRGrabber>();
        rawFoods.Add("Bread");
        rawFoods.Add("Meat");
        rawFoods.Add("Egg");
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(grabber.grabbedObject);
	
        if (grabber.grabbedObject != null && rawFoods.Contains(grabber.grabbedObject.tag))
        {
            handler.gotFood();
        } 

        if (grabber.grabbedObject == null && other.grabbedObject == null)
        {
            handler.droppedFood();
        }

    }
}
