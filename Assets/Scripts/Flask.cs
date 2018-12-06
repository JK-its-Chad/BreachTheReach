using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour {

    public int type = 0; //0 = heal, 1 = damage, 2 = speed, 3 = slow towers

    [SerializeField] GameObject potion;
    [SerializeField] ParticleSystem part;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask entity;

    [SerializeField] GameObject cloud;

    Color32 gas;

    void Awake () {

	}
	

	void Update ()
    {
        if (potion.GetComponent<MeshRenderer>().material.color == Color.white)
        {
            switch (type)
            {
                case 0:
                    potion.GetComponent<MeshRenderer>().material.color = Color.green;
                    part.startColor = Color.green;
                    break;
                case 1:
                    potion.GetComponent<MeshRenderer>().material.color = Color.red;
                    part.startColor = Color.red;
                    break;
                case 2:
                    potion.GetComponent<MeshRenderer>().material.color = Color.blue;
                    part.startColor = Color.blue;
                    break;
                case 3:
                    potion.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    part.startColor = Color.yellow;
                    break;
            }
        }

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (Collider en in Physics.OverlapSphere(transform.position, 5f, entity))
        {
            switch (type)
            {
                case 0://heal, green
                    if (en.GetComponent<EnemyAI>()) { en.GetComponent<EnemyAI>().health += 25; }
                    break;
                case 1://damage, red
                    if (en.GetComponent<SoldierAI>()) { en.GetComponent<SoldierAI>().health -= 25; }
                    break;
                case 2://speed, blue
                    if (en.GetComponent<EnemyAI>()) { en.GetComponent<EnemyAI>().slow += -10; }
                    break;
                case 3://slow towers, oranage
                    if(en.GetComponent<TowerAI>()) { en.GetComponent<TowerAI>().timer += 3f; }
                    break;
            }
        }
        GameObject after = Instantiate(cloud, transform.position, Quaternion.identity) as GameObject;
        gas = potion.GetComponent<MeshRenderer>().material.color;
        gas.a = 100;
        after.GetComponent<MeshRenderer>().material.color = gas;
        Destroy(after, 1f);
        Destroy(gameObject);
    }
}
