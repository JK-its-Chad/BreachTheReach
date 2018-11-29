using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    class Tower
    {
        public Transform location;
        public string type;
        public int level;

        public Tower(Transform plotSpot, string t, int lvl)
        {
            location = plotSpot;
            type = t;
            level = lvl;
        }
    }

    TurnManager bigBoss;
    [SerializeField] List<Tower> towers = new List<Tower>();
    [SerializeField] GameObject towerSpotList;
    [SerializeField] List<Transform> towerPlots = new List<Transform>();
    public int economicPoints = 0;

    [SerializeField] GameObject[] TowerTypes;
    int i = 1;


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
        if(economicPoints == 40)
        {
            Debug.Log(economicPoints);
        }

		if(bigBoss.roundOver)
        {
            if(economicPoints >= 10 && towers.Count < towerPlots.Count)
            {
                
                economicPoints -= 10;
                Debug.Log(economicPoints);
                BuyTower();
            }
        }
	}

    void BuyTower()
    {
        int spot = towerPlots.Count - i;
        int typeTow = Random.Range(0, TowerTypes.Length);

        GameObject tow = Instantiate(TowerTypes[typeTow], towerPlots[spot].position, towerPlots[spot].rotation) as GameObject;
        tow.name = TowerTypes[typeTow].name + " lvl:1";
        Tower addME = new Tower(towerPlots[spot], TowerTypes[typeTow].name, 1);
        towers.Add(addME);
        i++;

    }
    void UpgradeTower()
    {

    }
}
