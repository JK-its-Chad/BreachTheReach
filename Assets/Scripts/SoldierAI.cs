using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour
{

    [SerializeField] LayerMask enemyLayer;
    GameObject duelUnit = null;

    public int health = 100;
    public float speed = 2;
    public int damage = 2;
    public float attackTimer = 5;
    float timer = 0;

    Rigidbody rig;
    Vector3 spawn;

    // Use this for initialization
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!duelUnit)
        {
            NextTarget();
        }
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
                    duelUnit.GetComponent<EnemyAI>().health -= damage;
                    if (duelUnit.GetComponent<EnemyAI>().health < 0)
                    {
                        Destroy(duelUnit);
                    }
                }
            }

        }
        else
        {
            duelUnit.GetComponent<EnemyAI>().duelUnit = null;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!duelUnit && Vector3.Distance(transform.position, spawn) > .25f)
        {
            rig.AddForce((transform.position - spawn).normalized * -speed * 100);
        }
        rig.velocity = Vector3.zero;
    }

    public void NextTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 3f, enemyLayer);
        foreach(Collider en in enemies)
        {
            if(!duelUnit)
            {
                duelUnit = en.gameObject;
            }
        }
    }
}
