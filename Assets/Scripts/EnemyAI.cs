﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    GameObject pathList;
    //Transform[] pathTargets;
    [SerializeField] List<Transform> pathTargets;
    Vector3 target;
    int index = -1;

    [SerializeField] LayerMask enemyLayer;
    public GameObject duelUnit = null;
    bool attacking = false;
    public int health = 100;
    public float speed = 5;
    public int damage = 5;
    public float attackTimer = 5;
    float timer = 1;
    float sightRange = 3;

    bool ignoreTarget = false;
    bool attackTower = false;
    bool dodgeArrows = false;

    Rigidbody rig;

	void Awake ()
    {
        rig = GetComponent<Rigidbody>();
        pathList = GameObject.Find("PathList");
        //pathTargets = pathList.GetComponentsInChildren<Transform>();
		foreach(Transform t in pathList.GetComponentsInChildren<Transform>())
        {
            pathTargets.Add(t);
        }
        pathTargets.RemoveAt(0);
        NextTarget();
	}

    private void Update()
    {
        if(!duelUnit)
        {
            attacking = false;
            Collider[] enemies = Physics.OverlapSphere(transform.position, sightRange, enemyLayer);
            foreach (Collider en in enemies)
            {
                if (!duelUnit && en.gameObject.GetComponent<SoldierAI>())
                {
                    attacking = true;
                    duelUnit = en.gameObject;
                }
            }
        }
        if (attacking && duelUnit)
        {
            Combat();
        }

    }

    void FixedUpdate ()
    {
        if (index <= pathTargets.Count && !attacking)
        {
            if (Vector3.Distance(transform.position, target) > .5f && index != 12)
            {
                rig.AddForce((transform.position - target).normalized * -speed * 100);
            }
        }

        if (Vector3.Distance(transform.position, target) < 2 && index < pathTargets.Count - 1 && !attacking)
            NextTarget();

        rig.velocity = Vector3.zero;
	}

    void NextTarget()
    {
        index++;
        
        if(index == 11)
        {
            target.x += Random.Range(-20f, 20f);
            target.z += Random.Range(-5f, 5f);
            attacking = false;
        }
        else
        {
            target = pathTargets[index].position;
        }
        target.x += Random.Range(-2f, 2f);
        target.z += Random.Range(-2f, 2f);
        sightRange = 10f;
    }

    void Combat()
    {
        if (health > 0)
        {
            timer -= Time.deltaTime;
            if (duelUnit)
            {
                if (Vector3.Distance(transform.position, duelUnit.transform.position) > 2)
                {
                    transform.Translate((transform.position - duelUnit.transform.position).normalized * -speed * Time.deltaTime);
                    rig.velocity = Vector3.zero;
                }
                else if (timer <= 0)
                {
                    timer = attackTimer;
                    duelUnit.GetComponent<SoldierAI>().health -= damage;
                    if(duelUnit.GetComponent<SoldierAI>().health < 0)
                    {
                        attacking = false;
                        Destroy(duelUnit);
                        duelUnit = null;
                    }
                }
            }
            if(!duelUnit)
            {
                attacking = false;
                duelUnit = null;
            }
        }
        else
        {
            duelUnit.GetComponent<SoldierAI>().NextTarget();
            Destroy(gameObject);
        }
    }
}
