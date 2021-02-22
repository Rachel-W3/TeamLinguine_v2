using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RatScript : MonoBehaviour
{
    public Vector3 position = new Vector3(0f, 0f, 0f);
    public GameObject food;
    public string foodType;
    public TextMeshProUGUI score;
    public SandwhichScript sandwhichRecipe;
    public PotScript potScript;
    public TimerScript timer;
    public static int scoreInt = 0;
    public bool hasItem = false;
    public DishScript dishScript;
    public RawImage deliveryBubble;
    public GameObject ratModel;

    public Animator anim;

    public Rigidbody rat;

    private float speed = 5.0f; //originally 3.5, but it was unbearably slow...
    private Vector3 directionVector;

    // Start is called before the first frame update
    void Start()
    {
        scoreInt = 0;
        timer = GameObject.FindObjectOfType<TimerScript>();
        dishScript = GameObject.FindObjectOfType<DishScript>();
        deliveryBubble.gameObject.SetActive(false);
        directionVector = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        ratModel.transform.position = this.transform.position;

        if (timer.gameOver == false)
        {
            //Update Position Vector
            position = transform.position;

            //recieve input from the keyboard
            MovePlayer();

            //If ther ecipe is complete
            /*if (potScript.cooked)
            {
                
                //tScript.cooked = false;
            }*/
        }
        else
        {
            SceneManager.LoadScene("LevelComplete");
        }
    }

    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timer.gameOver == false)
        {

            if (collision.gameObject.tag == "Chef")
            {
                GameOver();
            }

            //If Colliding Object is a Pot
            if (collision.gameObject.tag == "Pot")
            {

                potScript = collision.gameObject.GetComponent<PotScript>();

                if(hasItem == true)
                {
                    foodType = food.tag;

                    Debug.Log("Collision detected! With " + foodType);

                    //Activate the pots
                    if (!potScript.progressSlider.gameObject.activeInHierarchy)
                    {
                        potScript.progressSlider.gameObject.SetActive(true);
                    }

                    if (foodType == "potato")
                    {
                        sandwhichRecipe.cookingPotato = true;
                        potScript = collision.gameObject.GetComponent<PotScript>();
                        if (!potScript.potatoIn)
                        {
                            potScript.potatoIn = true;
                            potScript.progressSlider.value += 0.33f;
                        }

                    }
                    else if (foodType == "onion")
                    {
                        sandwhichRecipe.cookingOnion = true;
                        potScript = collision.gameObject.GetComponent<PotScript>();
                        if (!potScript.onionIn)
                        {
                            potScript.onionIn = true;
                            potScript.progressSlider.value += 0.33f;
                        }
                    }
                    else if (foodType == "meat")
                    {
                        sandwhichRecipe.cookingMeat = true;
                        potScript = collision.gameObject.GetComponent<PotScript>();
                        if (!potScript.meatIn)
                        {
                            potScript.meatIn = true;
                            potScript.progressSlider.value += 0.33f;
                        }
                    }

                    food.GetComponent<FoodScript>().spawner.GetComponent<IngredientSpawnerScript>().spawned = false;

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
        
        bool isRunning = false;

        if (Input.GetKey("w"))
        {
            directionVector = new Vector3(-1, 0, 0);
            rat.AddForce(0, 0, -speed, ForceMode.Acceleration);
            isRunning = true;
        }
        if (Input.GetKey("a"))
        {
            directionVector = new Vector3(0, 0, -1);
            rat.AddForce(speed, 0, 0, ForceMode.Acceleration);
            isRunning = true;
        }
        if (Input.GetKey("s"))
        {
            directionVector = new Vector3(1, 0, 0);
            rat.AddForce(0, 0, speed, ForceMode.Acceleration);
            isRunning = true;
        }
        if (Input.GetKey("d"))
        {
            directionVector = new Vector3(0, 0, 1);
            rat.AddForce(-speed, 0, 0, ForceMode.Acceleration);
            isRunning = true;
        }
        if (Input.GetKeyDown("space"))
        {
            rat.AddForce(0, 10.0f, 0, ForceMode.Impulse); 
            isRunning = true;
        }
        ratModel.transform.rotation = Quaternion.LookRotation(directionVector);
        anim.SetBool("isRunning", isRunning);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("CaughtScreen");
    }

}
