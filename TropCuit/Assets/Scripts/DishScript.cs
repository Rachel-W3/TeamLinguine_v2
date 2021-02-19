using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishScript : MonoBehaviour
{
    public RatScript rs;
    //public GameObject ratGO;
    public bool pickup = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get acess to the Rat script
        rs = GameObject.Find("Rat").GetComponent<RatScript>();
        gameObject.SetActive(false);
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
}
