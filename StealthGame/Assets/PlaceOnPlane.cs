using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        } 
        else if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
