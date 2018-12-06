using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIinteract : MonoBehaviour {

    public OVRInput.Controller controller;
    public string buttonName;

    [SerializeField] Transform start;
    [SerializeField] Transform end;

    LineRenderer line;
    public LayerMask button;

    Quaternion lastRotation;
    Quaternion currentRotation;


    // Use this for initialization
    void Start ()
    {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis(buttonName) >= 0.9)
        {
            line.SetPosition(0, start.position);
            line.SetPosition(1, end.position);
            RaycastHit hit;
            if(Physics.Raycast(start.position, start.forward, out hit, 20, button))
            {
                if (hit.collider.gameObject.GetComponent<ButtonScript>())
                {
                    hit.collider.gameObject.GetComponent<ButtonScript>().Click();
                }
            }
        }
        if (Input.GetAxis(buttonName) < 0.9)
        {
            line.SetPosition(1, start.position);
        }
    }


}
