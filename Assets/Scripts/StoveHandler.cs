using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveHandler : MonoBehaviour {

    private CookFood script;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.CompareTag("Pan"))
        {
            script = obj.GetComponent<CookFood>();
            script.startCooking();
        }
    }
}
