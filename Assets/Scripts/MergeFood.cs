using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeFood : MonoBehaviour {

    public GameHandler handler;
    public GameObject burger;
    public GameObject omelette;

    private GameObject storedfood;
    private GameObject finalFood;

    private Hashtable foodPairs = new Hashtable();
    private bool hasFood = false;

    private bool withFinalFood = false;


	void Start () {
        foodPairs.Add("CookedMeat", false);
        foodPairs.Add("Bread", false);
        foodPairs.Add("FriedEgg", false);

        handler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
	}
	
	// Update is called once per frame
	void Update () {

        if (storedfood != null)
        {
            storedfood.transform.position = this.transform.position + (this.transform.forward * 0.01f);
            storedfood.transform.rotation = (this.transform.rotation * Quaternion.Euler(90, 0, 0));
        }

        if (finalFood != null)
        {
            finalFood.transform.position = this.transform.position + (this.transform.forward * 0.01f);
            finalFood.transform.rotation = (this.transform.rotation);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (foodPairs.Contains(obj.tag) && !obj.CompareTag("Furniture"))
        {

            if (!(bool)foodPairs[obj.tag])
            {
                foodPairs[obj.tag] = true;

                if (hasFood)
                {
                    GameObject success = Merge(obj.tag);

                    if (success != null)
                    {
                        Destroy(storedfood);
                        Destroy(obj);
                        spawnFood(success);
                        storedfood = null;
                    }
                }
                else
                {
                    storedfood = obj;
                    hasFood = true;
                    freezeObject(obj);
                    handler.waitToMerge();
                    //add text to complete merge
                }

            }
        }



    }

    private void freezeObject(GameObject obj)
    {
        Vector3 platePosition = this.transform.position;

		Destroy(obj.GetComponent<OVRGrabbable>());
		obj.GetComponent<Collider>().enabled = false; 
        obj.transform.position = new Vector3(platePosition.x, platePosition.y, platePosition.z);
        obj.transform.rotation = Quaternion.identity;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private GameObject Merge(string foodName)
    {
		Debug.Log(foodName);
		Debug.Log("storedFood: " + storedfood);
		
        switch (foodName)
        {
            case ("Bread"):
                if (storedfood.CompareTag("CookedMeat"))
                {
                    return burger;
                }
                break;
            case ("CookedMeat"):
                if (storedfood.CompareTag("Bread"))
                {
                    return burger;
                }
                else if (storedfood.CompareTag("FriedEgg"))
                {
                    return omelette;
                }
                break;
            case ("FriedEgg"):
                if (storedfood.CompareTag("CookedMeat"))
                {
                    return omelette;
                }
                break;
            default:
                return null;
        }
        return null;
    }

    private void spawnFood(GameObject food)
    {
        GameObject newFood = Instantiate(food, this.transform.position + (this.transform.forward) * 0.01f, food.transform.rotation);
        //newFood.GetComponent<Rigidbody>().isKinematic = false;
        newFood.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		Destroy(newFood.GetComponent<OVRGrabbable>());
        finalFood = newFood;
        withFinalFood = true;
        hasFood = false;

        handler.readyToServe();
    }

    public void deliverFood()
    {
        if (withFinalFood)
        {
            Dishes deliveredFood = Dishes.burger;

            if (finalFood.CompareTag("Burger"))
            {
                deliveredFood = Dishes.burger;

            } else if (finalFood.CompareTag("Omelette"))
            {
                deliveredFood = Dishes.omelette;
            }

            Destroy(finalFood);
            Destroy(gameObject);

            handler.resetGame(deliveredFood);

        } else if (storedfood.CompareTag("FriedEgg"))
        {
            Destroy(storedfood);
            Destroy(gameObject);
            handler.resetGame(Dishes.friedEgg);
        }
    }


}
