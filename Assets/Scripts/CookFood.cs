using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookFood : MonoBehaviour {

    public GameHandler handler;
    public Text displayText;
    public GameObject cookedMeat;
    public GameObject cookedEgg;
    public GameObject grillSound;

    private bool isCooking;
    private bool hasFood;
    private GameObject cookingFood = null;

    private float cookTime = 5.0f;

	// Use this for initialization
	void Start () {
        displayText.text = "Place food Here!!";
        grillSound.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 panPosition = this.transform.position;

        if (cookingFood != null)
        {
            cookingFood.transform.position = panPosition + (this.transform.forward * 0.1f);
            cookingFood.transform.rotation = (this.transform.rotation * Quaternion.Euler(90, 0, 0));
        }

        if (isCooking)
        {

            cookTime -= Time.deltaTime;
            if (cookTime <= 0)
            {
                endCooking();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
		
		if (hasFood) {
			return;
		}

        if (!isCooking && !obj.CompareTag("Furniture"))
        {
            if (obj.CompareTag("Meat") || obj.CompareTag("Egg"))
            {
                cookingFood = obj;
                freezeObject(cookingFood);
                displayText.text = "Cookable! Bring to Stove to start cooking";
                hasFood = true;

                handler.foodOnPan();
                
                return;
            }
            else
            {
                if (!obj.CompareTag("CookedMeat"))
                {
                    displayText.text = "This food is not cookable!";
                }
                handler.wrongFood();
            }
        } 

        hasFood = false; 


    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isCooking && !hasFood)
        {
            displayText.text = "Place food Here!!";
        }
    }

    private void freezeObject(GameObject obj)
    {
        Vector3 panPosition = this.transform.position;

        obj.transform.position = new Vector3(panPosition.x, panPosition.y + 0.1f, panPosition.z);
        obj.transform.rotation = Quaternion.identity;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        obj.GetComponent<Collider>().enabled = false;
		Destroy(obj.GetComponent<OVRGrabbable>());


    }

    public void startCooking()
    {
        if (hasFood && !isCooking)
        {
            isCooking = true;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.transform.rotation = Quaternion.Euler(-90, 0, 0); ;
            //this.GetComponent<Collider>().enabled = false;
			//this.GetComponent<OVRGrabbable>().enabled = false;
			//Destroy(this.GetComponent<OVRGrabbable>());
            displayText.text = "Cooking: wait 5 seconds!!";
            grillSound.SetActive(true);
        }

    }

    private void endCooking()
    {
        cookTime = 5.0f;
        //Finish Cooking!
        grillSound.SetActive(false);

		//this.GetComponent<OVRGrabbable>().enabled = true;
		//this.GetComponent<Collider>().enabled = true;
		//this.gameObject.AddComponent(typeof(OVRGrabbable));
		
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        GameObject newFood = null;

        if (cookingFood.CompareTag("Meat"))
        {
            Destroy(cookingFood);
            newFood = Instantiate(cookedMeat, this.transform.position + (this.transform.forward) * 0.1f, cookedMeat.transform.rotation);

        } else if (cookingFood.CompareTag("Egg"))
        {

            Destroy(cookingFood);
            newFood = Instantiate(cookedEgg, this.transform.position + (this.transform.forward) * 0.1f, cookedEgg.transform.rotation);
        }

        newFood.GetComponent<Rigidbody>().isKinematic = false;
        newFood.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


        
        displayText.text = "Finish Cooking!!";

        isCooking = false;
		hasFood = false;
        handler.panOnStove();
    }


}
