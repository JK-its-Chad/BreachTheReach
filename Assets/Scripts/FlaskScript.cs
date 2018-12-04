using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskScript : MonoBehaviour {

    [SerializeField] int type = 0; //0 = heal, 1 = damage, 2 = speed, 3 = slow towers
    [SerializeField] LayerMask ground;


	void Start () {
		
	}
	

	void Update ()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .75f, Vector3.zero, out hit, 1f, ground))
        {
            switch(type)
            {
                case 0://heal
                    break;
                case 1://damage
                    break;
                case 2://speed
                    break;
                case 3://slow towers
                    break;
            }
        }
	}
}
