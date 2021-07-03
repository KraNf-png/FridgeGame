using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float grabDistance = 5f, dropDistance = 10f, lerpSpeed = 200f;
    [SerializeField] Transform objectHolder;
    [SerializeField] Transform objectCube;

    Rigidbody grabbedRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbedRB)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));

            if (Input.GetMouseButtonDown(0))
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * dropDistance, ForceMode.VelocityChange);
                grabbedRB = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, grabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRB)
                    {
                        grabbedRB.velocity = (objectHolder.transform.position - objectCube.transform.position);
                        grabbedRB.isKinematic = true;
                    }
                }
            }
        }
    }
}
