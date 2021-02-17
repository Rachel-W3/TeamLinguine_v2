using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public Rigidbody rat;

    private float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindObjectOfType<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (sandwhichRecipe.cookedCheese && sandwhichRecipe.cookedBread && sandwhichRecipe.cookedMeat)
            {
                scoreInt += 10;
                score.text = scoreInt.ToString();

                sandwhichRecipe.cookedCheese = false;
                sandwhichRecipe.cookedBread = false;
                sandwhichRecipe.cookedMeat = false;
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
            if (collision.gameObject.tag == "Pot" && hasItem == true)
            {
                foodType = food.tag;

                Debug.Log("Collision detected! With " + foodType);

                if (foodType == "cheese")
                {
                    //sandwhichRecipe.cookingCheese = true;
                    potScript = collision.gameObject.GetComponent<PotScript>();
                    potScript.cheeseIn = true;
                }
                else if (foodType == "bread")
                {
                    //sandwhichRecipe.cookingBread = true;
                    potScript = collision.gameObject.GetComponent<PotScript>();
                    potScript.breadIn = true;
                }
                else if (foodType == "meat")
                {
                    //sandwhichRecipe.cookingMeat = true;
                    potScript = collision.gameObject.GetComponent<PotScript>();
                    potScript.meatIn = true;
                }

                Destroy((food));
                hasItem = false;
                scoreInt++;
                score.text = scoreInt.ToString();
            }
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
            rat.AddForce(0, 10.0f, 0, ForceMode.Impulse);
        }
    }

}
