using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerCombine : MonoBehaviour
{
    [SerializeField] GameObject burger;

    private int timer = 0;

    Vector3 burgerScale = new Vector3(0.16835f, 0.16835f, 0.16835f);
    
    private void OnTriggerEnter(Collider other)
    {
        Vector3 burgerPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
        if (other.name == "bread" && timer < 1)
        {
            timer++;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            this.burger.transform.localScale = burgerScale;
            Instantiate(burger, burgerPosition, Quaternion.identity);       
        }
    }
}
