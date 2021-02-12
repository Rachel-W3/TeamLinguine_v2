using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SandwhichScript : MonoBehaviour
{
    public bool cookingCheese = false;
    public bool cookingBread = false;
    public bool cookingMeat = false;

    public bool cookedCheese = false;
    public bool cookedBread = false;
    public bool cookedMeat = false;

    private float cheeseTime = 0;
    private float breadTime = 0;
    private float meatTime = 0;

    public GameObject cheeseCooking;
    public GameObject breadCooking;
    public GameObject meatCooking;

    // Start is called before the first frame update
    void Start()
    {
        cheeseCooking.SetActive(false);
        breadCooking.SetActive(false);
        meatCooking.SetActive(false);
        //cheeseCooking.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cookingCheese == true && cookedCheese == false)
        {
            cheeseCooking.SetActive(true);

            cheeseTime += Time.deltaTime;

            if (cheeseTime > 10)
            {
                cheeseTime = 0;
                cookedCheese = true;
                cookingCheese = false;
                cheeseCooking.SetActive(false);
            }
        }

        if (cookingBread == true && cookedBread == false)
        {
            breadCooking.SetActive(true);

            breadTime += Time.deltaTime;

            if (breadTime > 10)
            {
                breadTime = 0;
                cookedBread = true;
                cookingBread = false;
                breadCooking.SetActive(false);
            }
        }

        if (cookingMeat == true && cookedMeat == false)
        {
            meatCooking.SetActive(true);

            meatTime += Time.deltaTime;

            if (meatTime > 10)
            {
                meatTime = 0;
                cookedMeat = true;
                cookingMeat = false;
                meatCooking.SetActive(false);
            }
        }
        
    }
}
