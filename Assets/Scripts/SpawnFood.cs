using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
	
	public GameObject foodSpawn;
	public GameObject food;

    private Color originalColor;
    private Color pressedColor;


	// Use this for initialization
	void Start () {
        originalColor = this.GetComponent<Renderer>().material.color;
        pressedColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bread Trigger!");
        if (other.gameObject.CompareTag("Hand"))
        {
            GameObject newFood = Instantiate(food, new Vector3(foodSpawn.transform.position.x - 0.25f, foodSpawn.transform.position.y + 0.6f, foodSpawn.transform.position.z - 0.25f), food.transform.rotation);
            newFood.GetComponent<Rigidbody>().isKinematic = false;
            newFood.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }


        this.GetComponent<Renderer>().material.color = pressedColor;

    }

    void OnTriggerExit(Collider other)
    {
        this.GetComponent<Renderer>().material.color = originalColor;
    }

}
