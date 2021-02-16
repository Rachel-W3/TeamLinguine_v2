using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    //Tracks the progress of the pot
    float progress;
    bool breadIn;
    bool cheeseIn;
    bool meatIn;


    
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        meatIn = false;
        breadIn = false;
        cheeseIn = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
