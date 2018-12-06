using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour {

    public float rangeZForward = 10;
    public float rangeZBack = 10;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(transform.position.z < rangeZBack)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, rangeZBack);
        }
        if (transform.position.z > rangeZForward)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, rangeZForward);
        }
    }
}
