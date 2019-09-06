using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverFood : MonoBehaviour {

    private MergeFood script;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        GameObject obj = other.gameObject;

        if (obj.CompareTag("Plate"))
        {
            script = obj.GetComponent<MergeFood>();
            script.deliverFood();
        }
    }
}
