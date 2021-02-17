using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    //Tracks the progress of the pot
    float progress;
    public bool breadIn;
    public bool cheeseIn;
    public bool meatIn;
    public bool cooking;
    GameObject progressSlider;


    
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        meatIn = false;
        breadIn = false;
        cheeseIn = false;
        cooking = false;

        //find the slider of the child canvas
        //this is hard and I don't get it
        
    }

    // Update is called once per frame
    void Update()
    {
        //if all the ingredients are in, reset progress bar to 0 and start the progress timer
        if(!cooking && meatIn && breadIn && cheeseIn)
        {
            cooking = true;
        }

        if (cooking)
        {
            //update the slider once per frame till it's done
        }
    }
}
