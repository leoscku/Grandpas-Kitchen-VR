using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    start,
    foodToPan,
    droppedFood,
    panToStove,
    stoveToPlate,
    waitToMerge,
    plateToServe,
    gameOver
}

public enum Dishes
{
    burger,
    omelette,
    friedEgg
}

public class GameHandler : MonoBehaviour {

    public Text instruction;
    public Text deliverCountText;

    public UpVector arrow;
    public GameObject breadSpawn;
    public GameObject plateSpawn;
    public GameObject pan;
    public GameObject stove;
    public GameObject serve;
    public GameObject mafia;
    public DemandDisplay demandDisplay;

    private GameState currState;
    private Dishes nextDish;
    private Vector3 breadLoc;
    private Vector3 plateLoc;
    private Vector3 panLoc;
    private Vector3 stoveLoc;
    private Vector3 serveLoc;
    private Vector3 mafiaLoc;

    private int deliveredOrders = 0;

    private Random random = new Random();
    

	// Use this for initialization
	void Start () {
        currState = GameState.start;

        nextDish = (Dishes)Random.Range(0, 3);

        breadLoc = breadSpawn.transform.position;
        plateLoc = plateSpawn.transform.position;
        panLoc = pan.transform.position;
        stoveLoc = stove.transform.position;
        serveLoc = serve.transform.position;
        instruction.text = "Pick up food";
        deliverCountText.text = "Delivered: 0";

    }
	
	// Update is called once per frame
	void Update () {

        panLoc = pan.transform.position;

		//Debug.Log(currState);
        switch (currState)
        {
            case GameState.start:
                arrow.updateDirection(breadLoc);
                break;
			case GameState.droppedFood:
				arrow.updateDirection(breadLoc);
				break;
            case GameState.foodToPan:
                arrow.updateDirection(panLoc);
                break;
            case GameState.panToStove:
                arrow.updateDirection(stoveLoc);
                break;
            case GameState.stoveToPlate:
                arrow.updateDirection(plateLoc);
                break;
			case GameState.waitToMerge:
				arrow.updateDirection(breadLoc);
				break;
            case GameState.plateToServe:
                arrow.updateDirection(serveLoc);
                break;
            case GameState.gameOver:
                arrow.updateDirection(mafiaLoc);
                break;
            default:
                break;
        }

        switch(nextDish)
        {
            case Dishes.burger:
                demandDisplay.displayBurgerDemand();
                break;
            case Dishes.omelette:
                demandDisplay.displayOmeletteDemand();
                break;
            case Dishes.friedEgg:
                demandDisplay.displayFriedEggDemand();
                break;

        }

    }

    public void gotFood()
    {
        if (currState == GameState.start || currState == GameState.droppedFood)
        {
            instruction.text = "Bring a cookable food to Pan!";
            currState = GameState.foodToPan;
        }
    }

    public void foodOnPan()
    {
        if (currState == GameState.foodToPan || currState == GameState.droppedFood)
        {
            instruction.text = "Bring pan to stove!";
            currState = GameState.panToStove;
        }
    }

    public void wrongFood()
    {
        if (currState == GameState.foodToPan)
        {
            instruction.text = "Wrong food! Go find another one!";
            currState = GameState.start;
        }
    }

    public void droppedFood()
    {
        if (currState == GameState.foodToPan)
        {
            instruction.text = "Pick up food";
            currState = GameState.droppedFood;
        }
    }

    public void panOnStove()
    {
        if (currState == GameState.panToStove)
        {
            instruction.text = "Bring cooked food to a plate!";
            currState = GameState.stoveToPlate;
        }
    }

    public void waitToMerge()
    {
        if (currState == GameState.stoveToPlate)
        {
            instruction.text = "Bring the other food to merge! Or deliver if you think it is done!";
            currState = GameState.waitToMerge;
        }
    }

    public void readyToServe()
    {
        if (currState == GameState.waitToMerge || currState == GameState.stoveToPlate)
        {
            instruction.text = "Bring food to serve area!";
            currState = GameState.plateToServe;
        }
    }



    public void resetGame(Dishes deliveredFood)
    {
        if (deliveredFood == nextDish && currState != GameState.gameOver)
        {
            instruction.text = "Pick up food";
            deliveredOrders++;
            deliverCountText.text = "Delivered: " + deliveredOrders;
            currState = GameState.start;

            nextDish = (Dishes)Random.Range(0, 3);
        }
    }

    public void gameOver()
    {
        currState = GameState.gameOver;
        instruction.text = "You have dropped your pan, and is now unable to cook. Game Over.";
        deliverCountText.text = "Delivered: 0";
    }

}
