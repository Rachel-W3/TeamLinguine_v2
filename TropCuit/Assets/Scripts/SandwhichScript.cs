using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SandwhichScript : MonoBehaviour
{
    public bool cookingPotato = false;
    public bool cookingOnion = false;
    public bool cookingMeat = false;

    private float potatoTime = 0;
    private float onionTime = 0;
    private float meatTime = 0;

    public GameObject potatoCooking;
    public GameObject onionCooking;
    public GameObject meatCooking;

    // Start is called before the first frame update
    void Start()
    {
        potatoCooking.SetActive(false);
        onionCooking.SetActive(false);
        meatCooking.SetActive(false);
        //potatoCooking.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cookingPotato == true)
        {
            potatoCooking.SetActive(true);

            /*potatoTime += Time.deltaTime;

            if (potatoTime > 10)
            {
                potatoTime = 0;
                cookedpotato = true;
                cookingpotato = false;
                potatoCooking.SetActive(false);
            }*/
        }
        else
        {
            potatoCooking.SetActive(false);
        }

        if (cookingOnion == true)
        {
            onionCooking.SetActive(true);

            /*onionTime += Time.deltaTime;

            if (onionTime > 10)
            {
                onionTime = 0;
                cookedonion = true;
                cookingonion = false;
                onionCooking.SetActive(false);
            }*/
        }
        else
        {
            onionCooking.SetActive(false);
        }

        if (cookingMeat == true)
        {
            meatCooking.SetActive(true);

            /*meatTime += Time.deltaTime;

            if (meatTime > 10)
            {
                meatTime = 0;
                cookedMeat = true;
                cookingMeat = false;
                meatCooking.SetActive(false);
            }*/
        }
        else
        {
            meatCooking.SetActive(false);
        }

    }
}
