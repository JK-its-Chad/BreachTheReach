using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    public int health = 1000;
    [SerializeField] TextMesh hpBar;

    TurnManager bigBoss;
    [SerializeField] List<GameObject> towers = new List<GameObject>();
    [SerializeField] GameObject towerSpotList;
    [SerializeField] List<Transform> towerPlots = new List<Transform>();
    public int economicPoints = 0;

    [SerializeField] GameObject[] TowerTypes;
    int buy = 1;
    int up1 = 0;
    int up2 = 0;


    void Start ()
    {
        bigBoss = GameObject.Find("Manager").GetComponent<TurnManager>();
        economicPoints += 30;
        foreach(Transform t in towerSpotList.GetComponentsInChildren<Transform>())
        {
            towerPlots.Add(t);
        }
        towerPlots.RemoveAt(0);

	}

	void Update ()
    {
        hpBar.text = "";
        int i = Mathf.RoundToInt(health / 100);
        for(int a = 0; a < i; a++ )
        {
            hpBar.text += "|";
        }
        if(health < 0)
        {
            health = 0;
        }

		if(bigBoss.roundOver)
        {
            if(economicPoints >= 10 && buy <= towerPlots.Count)
            {
                economicPoints -= 10;
                BuyTower();
            }
            if(economicPoints >= 20 && towers.Count == towerPlots.Count && up1 < towerPlots.Count)
            {
                economicPoints -= 20;
                UpgradeTower1();
            }
            if (economicPoints >= 30 && towers.Count == towerPlots.Count && up2 < towerPlots.Count)
            {
                economicPoints -= 30;
                UpgradeTower2();
            }
            if (economicPoints > 0 &&  health <= 995)
            {
                economicPoints--;
                health += 5;
            }
        }
	}

    void BuyTower()
    {
        int spot = towerPlots.Count - buy;
        int typeTow = Random.Range(0, TowerTypes.Length);

        GameObject tow = Instantiate(TowerTypes[typeTow], towerPlots[spot].position, towerPlots[spot].rotation) as GameObject;
        TowerClass addME = new TowerClass(towerPlots[spot], TowerTypes[typeTow].name, 1);
        tow.GetComponent<TowerAI>().me = addME;
        towers.Add(tow);
        buy++;

    }
    void UpgradeTower1()
    {
        towers[up1].GetComponent<TowerAI>().me.level++;
        up1++;
    }

    void UpgradeTower2()
    {
        towers[up2].GetComponent<TowerAI>().me.level++;
        up2++;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            other.gameObject.GetComponent<EnemyAI>().DamageGate(this);
        }
    }
}
