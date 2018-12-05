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


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySupport()
    {
        transform.position = Support.position;
        FlaskBelt.SetActive(true);
    }

    public void PlayAttack()
    {
        transform.position = Attack.position;
        LSheild.SetActive(true);
        RSheild.SetActive(true);
    }

    public void RoundOver()
    {
        transform.position = Tower.position;
        FlaskBelt.SetActive(false);
        LSheild.SetActive(false);
        RSheild.SetActive(false);
    }
}
