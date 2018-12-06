using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] Transform Support;
    [SerializeField] Transform Attack;
    [SerializeField] Transform Tower;

    [SerializeField] GameObject FlaskBelt;
    [SerializeField] GameObject LSheild;
    [SerializeField] GameObject RSheild;

    [SerializeField] GameObject cam;
    float angle = 0;

    bool attacking = false;
    public bool supporting = false;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("RightJoyX") > .9 || Input.GetAxis("RightJoyX") < -.9)
        {
            angle += Input.GetAxis("RightJoyX");
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + angle * .5f, 0));
        }

        if (attacking)
        {
            Vector3 MoveDir = new Vector3(Input.GetAxis("LeftJoyX"), 0, Input.GetAxis("LeftJoyY")) * Time.deltaTime;
            Vector3 cameraRot = cam.transform.rotation.eulerAngles;
            transform.position += Quaternion.Euler(0, cameraRot.y, 0) * MoveDir;

            if(Input.GetAxis("RRay") >= 0.9)
            {
                RSheild.transform.localScale = new Vector3(2, 1, 2);
            }
            else if (Input.GetAxis("RRay") < 0.9)
            {
                RSheild.transform.localScale = new Vector3(.5f, 1, .5f);
            }

            if (Input.GetAxis("LRay") >= 0.9)
            {
                LSheild.transform.localScale = new Vector3(2, 1, 2);
            }
            else if (Input.GetAxis("LRay") < 0.9)
            {
                LSheild.transform.localScale = new Vector3(.5f, 1, .5f);
            }
        }

        if(supporting)
        {

        }

    }

    public void PlaySupport()
    {
        transform.position = Support.position;
        supporting = true;
        FlaskBelt.SetActive(true);
    }

    public void PlayAttack()
    {
        transform.position = Attack.position;
        attacking = true;
        LSheild.SetActive(true);
        RSheild.SetActive(true);
    }

    public void RoundOver()
    {
        transform.position = Tower.position;
        attacking = false;
        supporting = false;
        FlaskBelt.SetActive(false);
        LSheild.SetActive(false);
        RSheild.SetActive(false);
    }

    public void ToSupport()
    {
        if (supporting)
        {
            transform.position = Support.position;
        }
    }
}
