using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodScript : MonoBehaviour
{
    public RatScript rs;
    public GameObject ratGO;
    public RawImage foodBubble;
    public bool pickup = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get acess to the Rat and its script
        ratGO = GameObject.Find("Rat");
        rs = ratGO.GetComponent<RatScript>();
        foodBubble = GetComponentInChildren<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        //If Food has been gathered, move it with Rat but slightly above
        if (pickup)
        {
            transform.position = new Vector3(rs.position.x, rs.position.y + 2, rs.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //If Colliding Object is a Rat
        if (other == ratGO.GetComponent<Collider>())
        {
            //Ensure that Rat is not already carrying an item
            if(rs.hasItem == false)
            {
                pickup = true;
                rs.hasItem = true;
                rs.food = this.gameObject;
                rs.foodType = this.tag;
                foodBubble.gameObject.SetActive(false);
            }

        }
    }
}
