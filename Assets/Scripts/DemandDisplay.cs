using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandDisplay : MonoBehaviour {
	
	public GameObject BurgerDisplay; 
	public GameObject OmeletteDisplay;
	public GameObject FriedEggDisplay; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void displayBurgerDemand() {
		BurgerDisplay.SetActive(true); 
		OmeletteDisplay.SetActive(false); 
		FriedEggDisplay.SetActive(false); 
	}
	
	public void displayOmeletteDemand() {
		BurgerDisplay.SetActive(false); 
		OmeletteDisplay.SetActive(true); 
		FriedEggDisplay.SetActive(false); 
	}
	
	public void displayFriedEggDemand() {
		BurgerDisplay.SetActive(false); 
		OmeletteDisplay.SetActive(false); 
		FriedEggDisplay.SetActive(true); 
	}
}
