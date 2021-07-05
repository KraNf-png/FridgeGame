using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieCombine : MonoBehaviour
{
    [SerializeField] GameObject pie;

    private int timer = 0;

    Vector3 pieScale = new Vector3(0.1205512f, 0.1205512f, 0.1205512f);

    private void OnTriggerEnter(Collider other)
    {
        Vector3 burgerPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.1f, gameObject.transform.position.z);
        if (other.name == "flour" && timer < 1)
        {
            timer++;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            this.pie.transform.localScale = pieScale;
            Instantiate(pie, burgerPosition, Quaternion.identity);
        }
    }
}
