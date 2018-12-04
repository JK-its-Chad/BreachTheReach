using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour {

    public TowerClass me;

    [SerializeField] GameObject lvl1;
    [SerializeField] GameObject lvl2;
    [SerializeField] GameObject lvl3;

    [SerializeField] Transform targetPoint;
    [SerializeField] LayerMask defender;
    [SerializeField] LayerMask attacker;

    [SerializeField] GameObject soldier;

    float timerMAX = 5f;
    float timer = 0;


    void Start ()
    {
        name = me.type + "_lvl: " + me.level;
    }
	

	void Update ()
    {
        if (me.level >= 2 && lvl2.activeSelf == false)
        {
            lvl2.SetActive(true);
        }
        if (me.level >= 3 && lvl3.activeSelf == false)
        {
            lvl3.SetActive(true);
        }

        switch(me.type)
        {
            case "SoldierTower":
                if(Physics.OverlapSphere(targetPoint.position, 5, defender).Length < 3)
                {
                    timer -= Time.deltaTime;
                    if(timer <= 0)
                    {
                        Vector3 rando = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                        GameObject newRecruit = Instantiate(soldier, targetPoint.position + rando, Quaternion.Euler(0, 180, 0)) as GameObject;
                        newRecruit.GetComponent<SoldierAI>().health += me.level * 10;
                        newRecruit.GetComponent<SoldierAI>().damage += me.level;
                        timer = timerMAX - ((float)me.level / 10);
                    }
                }
                break;
            case "ArcherTower":
                break;
            case "WizardTower":
                break;
        }
    }
}
