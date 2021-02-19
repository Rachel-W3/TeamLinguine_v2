using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotScript : MonoBehaviour
{
    //Tracks the progress of the pot
    float progress;
    public bool onionIn;
    public bool potatoIn;
    public bool meatIn;
    public bool cooking;
    public bool cooked = false;
    public Slider progressSlider;
    public RawImage potBubble;
    public RawImage soupBubble;
    
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        meatIn = false;
        onionIn = false;
        potatoIn = false;
        cooking = false;

        potBubble.gameObject.SetActive(true);
        soupBubble.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //if all the ingredients are in, reset progress bar to 0 and start the progress timer
        if(!cooking && meatIn && onionIn && potatoIn)
        {
            cooking = true;
            progressSlider.value = 0f;
            potBubble.gameObject.SetActive(false);
        }

        if (cooking)
        {
            if (progressSlider.value < 1f)
            {
                progressSlider.value += 0.0005f;
            }
           
        }

        if(progressSlider.value >= 1)
        {
            cooked = true;
            cooking = false;
            //progressSlider.value = 0;
            meatIn = false;
            onionIn = false;
            potatoIn = false;

            soupBubble.gameObject.SetActive(true);
        }
    }
}
