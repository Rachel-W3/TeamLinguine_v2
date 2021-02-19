using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawnerScript : MonoBehaviour {

    public GameObject ingredientToSpawn;

    [HideInInspector]
    public bool spawned;

    // Start is called before the first frame update
    void Start() {
        spawned = false;
        ingredientToSpawn.transform.position = gameObject.transform.position;
        SpawnIngredient();
    }

    // Update is called once per frame
    void Update() {
        if (!spawned) {
            SpawnIngredient();
        }
    }

    void SpawnIngredient() {
        spawned = true;
        ingredientToSpawn.GetComponent<FoodScript>().spawner = this;
        ingredientToSpawn.transform.position = gameObject.transform.position;
        Instantiate(ingredientToSpawn);
    }
}