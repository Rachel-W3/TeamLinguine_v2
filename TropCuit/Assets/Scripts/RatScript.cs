using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RatScript : MonoBehaviour
{
    public Vector3 position = new Vector3(0f, 0f, 0f);
    public GameObject food;
    public string foodType;
    public TextMeshProUGUI score;
    public SandwhichScript sandwhichRecipe;
    public TextMeshProUGUI gameOverText;
    public PotScript potScript;
    public TimerScript timer;
    private int scoreInt = 0;
    public bool hasItem = false;
    public DishScript dishScript;
    public RawImage deliveryBubble;
    public GameObject ratModel;

    public Rigidbody rat;

    private float speed = 10f; //3.5

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindObjectOfType<TimerScript>();
        dishScript = GameObject.FindObjectOfType<DishScript>();
        deliveryBubble.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        ratModel.transform.position = this.transform.position;

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (timer.gameOver == false)
        {
            //Update Position Vector
            position = transform.position;

            //recieve input from the keyboard
            MovePlayer();

            //If ther ecipe is complete
            if (potScript.cooked)
            {
                
                //tScript.cooked = false;
            }
        }
        else
        {
            gameOverText.text = "Game Over! \nScore: " + scoreInt.ToString() + "\nPress Esc to Exit";
        }
    }

    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timer.gameOver == false)
        {
            //If Colliding Object is a Pot
            if (collision.gameObject.tag == "Pot")
            {
                if(hasItem == true)
                {
                    foodType = food.tag;

                    Debug.Log("Collision detected! With " + foodType);

                    if (foodType == "potato")
                    {
                        sandwhichRecipe.cookingPotato = true;
                        potScript = collision.gameObject.GetComponent<PotScript>();
                        potScript.potatoIn = true;
                        potScript.progressSlider.value += 0.33f;
                    }
                    else if (foodType == "onion")
                    {
                        sandwhichRecipe.cookingOnion = true;
                        potScript = collision.gameObject.GetComponent<PotScript>();
                        potScript.onionIn = true;
                        potScript.progressSlider.value += 0.33f;
                    }
                    else if (foodType == "meat")
                    {
                        sandwhichRecipe.cookingMeat = true;
                        potScript = collision.gameObject.GetComponent<PotScript>();
                        potScript.meatIn = true;
                        potScript.progressSlider.value += 0.33f;
                    }

                    Destroy((food));
                    hasItem = false;
                    scoreInt++;
                    score.text = scoreInt.ToString();
                }
                else if(potScript.cooked == true && hasItem == false)
                {
                    dishScript.pickup = true;
                    dishScript.gameObject.SetActive(true);
                    hasItem = true;

                    potScript.cooked = false;
                    potScript.soupBubble.gameObject.SetActive(false);
                    potScript.potBubble.gameObject.SetActive(true);
                    deliveryBubble.gameObject.SetActive(true);

                    sandwhichRecipe.cookingPotato = false;
                    sandwhichRecipe.cookingOnion = false;
                    sandwhichRecipe.cookingMeat = false;
                }

            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "servingCart" && dishScript.pickup == true)
        {
                hasItem = false;
                dishScript.gameObject.SetActive(false);
                deliveryBubble.gameObject.SetActive(false);
                scoreInt += 10;
                score.text = scoreInt.ToString();
        }
    }

    private void MovePlayer()
    {
        if (Input.GetKey("w"))
        {
            rat.AddForce(0, 0, -speed, ForceMode.Acceleration);
        }
        if (Input.GetKey("a"))
        {
            rat.AddForce(speed, 0, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey("s"))
        {
            rat.AddForce(0, 0, speed, ForceMode.Acceleration);
        }
        if (Input.GetKey("d"))
        {
            rat.AddForce(-speed, 0, 0, ForceMode.Acceleration);
        }
        if (Input.GetKeyDown("space"))
        {
            rat.AddForce(0, 11.0f, 0, ForceMode.Impulse); //10
        }
    }

}
