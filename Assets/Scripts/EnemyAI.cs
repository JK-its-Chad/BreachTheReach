using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] GameObject pathList;
    [SerializeField] List<Transform> pathTargets;
    Vector3 target;
    int index = -1;
    public float speed = 5;

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
	
	void FixedUpdate ()
    {
        if (Vector3.Distance(transform.position, target) < 2 && index < pathTargets.Count) NextTarget();
        if (index < pathTargets.Count) rig.AddForce((transform.position - target).normalized * -speed * 100);
        rig.velocity = Vector3.zero;
	}

    void NextTarget()
    {
        index++;
        target = pathTargets[index].position;
        target.x += Random.Range(-2f, 2f);
        target.z += Random.Range(-2f, 2f);
    }
}
