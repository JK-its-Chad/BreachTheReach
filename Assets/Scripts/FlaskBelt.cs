using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskBelt : MonoBehaviour {

    [SerializeField] GameObject Potion;

    [SerializeField] Transform Spot1;
    [SerializeField] Transform Spot2;
    [SerializeField] Transform Spot3;
    [SerializeField] Transform Spot4;

    float timer1 = 0;
    float timer2 = 0;
    float timer3 = 0;
    float timer4 = 0;
    float timerMAX = 5f;

    void Awake ()
    {
		
	}

	void Update ()
    {
		if(!Spot1.GetComponentInChildren<Flask>())
        {
            timer1 -= Time.deltaTime;
            if(timer1 <= 0)
            {
                GameObject pot1 = Instantiate(Potion, Spot1) as GameObject;
                pot1.name = "Potion1";
                pot1.GetComponent<Flask>().type = Random.Range(0, 4);
                pot1.GetComponent<Rigidbody>().isKinematic = true;
                timer1 = timerMAX;
            }
        }
        if (!Spot2.GetComponentInChildren<Flask>())
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                GameObject pot2 = Instantiate(Potion, Spot2) as GameObject;
                pot2.name = "Potion2";
                pot2.GetComponent<Flask>().type = Random.Range(0, 4);
                pot2.GetComponent<Rigidbody>().isKinematic = true;
                timer2 = timerMAX;
            }
        }
        if (!Spot3.GetComponentInChildren<Flask>())
        {
            timer3 -= Time.deltaTime;
            if (timer3 <= 0)
            {
                GameObject pot3 = Instantiate(Potion, Spot3) as GameObject;
                pot3.name = "Potion3";
                pot3.GetComponent<Flask>().type = Random.Range(0, 4);
                pot3.GetComponent<Rigidbody>().isKinematic = true;
                timer3 = timerMAX;
            }
        }
        if (!Spot4.GetComponentInChildren<Flask>())
        {
            timer4 -= Time.deltaTime;
            if (timer4 <= 0)
            {
                GameObject pot4 = Instantiate(Potion, Spot4) as GameObject;
                pot4.name = "Potion4";
                pot4.GetComponent<Flask>().type = Random.Range(0, 4);
                pot4.GetComponent<Rigidbody>().isKinematic = true;
                timer4 = timerMAX;
            }
        }
    }
}
