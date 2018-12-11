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

    EnemyAI target;

    float timerMAX = 5f;
    public float timer = 0;
    int damage = 10;


    void Start()
    {
        name = me.type + "_lvl: " + me.level;
        switch (me.type)
        {
            case "SoldierTower":
                timerMAX = 8f;
                break;
            case "ArcherTower":
                timerMAX = 2f;
                break;
            case "WizardTower":
                timerMAX = 5f;
                break;
        }
    }
	

	void Update ()
    {
        timer -= Time.deltaTime;

        if (me.level >= 3)
        {
            lvl3.SetActive(true);
            name = me.type + "_lvl: " + me.level;
        }
        if (me.level >= 2)
        {
            lvl2.SetActive(true);
            name = me.type + "_lvl: " + me.level;
        }


        switch(me.type)
        {
            case "SoldierTower":
                if(Physics.OverlapSphere(targetPoint.position, 5, defender).Length < 3)
                {
                    if(timer <= 0)
                    {
                        Vector3 rando = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                        GameObject newRecruit = Instantiate(soldier, targetPoint.position + rando, Quaternion.Euler(0, 180, 0)) as GameObject;
                        newRecruit.GetComponent<SoldierAI>().health += me.level * 10;
                        newRecruit.GetComponent<SoldierAI>().damage += me.level;
                        timer = timerMAX - me.level;
                    }
                }
                break;
            case "ArcherTower":
                if(timer <= 0)
                {
                    foreach (Collider en in Physics.OverlapSphere(targetPoint.position, 20))
                    {
                        if (en.gameObject.GetComponent<EnemyAI>())
                        {
                            if (!target)
                            {
                                target = en.gameObject.GetComponent<EnemyAI>();
                                if (en.gameObject.GetComponent<EnemyAI>().health < target.health)
                                {
                                    target = en.gameObject.GetComponent<EnemyAI>();
                                }
                                if (Vector3.Distance(target.transform.position, targetPoint.position) > 20f)
                                {
                                    target = null;
                                }
                            }
                        }
                    }
                    if(target)
                    {
                        target.health -= (damage + me.level) * 2;
                    }
                    timer = timerMAX / me.level;
                    if (target && target.attackTower)
                    {
                        timer += timerMAX / 2;
                    }
                }
                break;
            case "WizardTower":
                if (timer <= 0)
                {
                    bool slowAttack = false;
                    foreach(Collider en in Physics.OverlapSphere(targetPoint.position, 20))
                    {
                        if(en.GetComponent<EnemyAI>())
                        {
                            en.gameObject.GetComponent<EnemyAI>().health -= damage + me.level;
                            en.gameObject.GetComponent<EnemyAI>().slow = me.level;
                            if(en.gameObject.GetComponent<EnemyAI>().attackTower)
                            {
                                slowAttack = true;
                            }
                        }
                    }
                    timer = timerMAX - me.level;
                    if(slowAttack)
                    {
                        timer += timerMAX / 2;
                    }
                }
                break;
        }
    }
}
