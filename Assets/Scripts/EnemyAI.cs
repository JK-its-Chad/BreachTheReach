using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] GameObject pathList;
    [SerializeField] List<Transform> pathTargets;
    public Vector3 target;
    int index = -1;
    public float health = 100;
    public float speed = 5;

    bool ignoreTarget = false;
    bool attackTower = false;
    bool dodgeArrows = false;

    bool attacking = false;
    bool freeWalk = false;
    [SerializeField] LayerMask enemyLayer;
    GameObject duelUnit;

    Rigidbody rig;

	void Start ()
    {
        rig = GetComponent<Rigidbody>();
		foreach(Transform t in pathList.GetComponentsInChildren<Transform>())
        {
            pathTargets.Add(t);
        }
        pathTargets.RemoveAt(0);
        NextTarget();
	}

    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 3, Vector3.zero, enemyLayer);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.layer == enemyLayer && !freeWalk && !attacking)
            {
                attacking = true;
                duelUnit = hit.transform.gameObject;
                //hit.transform.gameObject.GetComponent<>
            }
        }
        if(attacking && duelUnit != null && !freeWalk)
        {
            Combat();
        }
    }

    void FixedUpdate ()
    {
        if (Vector3.Distance(transform.position, target) < 2 && index < pathTargets.Count && !attacking) NextTarget();
        if (index <= pathTargets.Count - 1 && !attacking) rig.AddForce((transform.position - target).normalized * -speed * 100);
        rig.velocity = Vector3.zero;
	}

    void NextTarget()
    {
        index++;
        freeWalk = false;
        target = pathTargets[index].position;
        if(index == 11)
        {
            target.x += Random.Range(-20f, 20f);
            target.z += Random.Range(-10f, 10f);
            attacking = true;
        }
        target.x += Random.Range(-2f, 2f);
        target.z += Random.Range(-2f, 2f);
    }

    void Combat()
    {
        
    }
}
