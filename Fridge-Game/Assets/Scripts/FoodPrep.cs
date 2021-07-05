using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPrep : MonoBehaviour
{
    [SerializeField] GameObject burger;

    private int timer = 0;

    Vector3 burgerScale = new Vector3(0.16835f, 0.16835f, 0.16835f);
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "bread" && timer < 1)
        {
            timer++;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            this.burger.transform.localScale = burgerScale;
            Instantiate(burger, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
