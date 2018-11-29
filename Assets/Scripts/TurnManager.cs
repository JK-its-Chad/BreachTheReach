using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    ComputerAI comp;

    public int difficulty = 0;
    public int currentRound = 1;
    float timer = 2f;

    [SerializeField] Transform enemySpawn;
    EnemyAI[] liveEnemies;
    GameObject[] minionsMade; 
    int enemyTypes = 0;
    float enemySpawnTime = 1f;

    bool ready = false;
    public bool roundOver = false;
    public bool roundRunning = false;


	void Start ()
    {
        comp = GameObject.Find("Computer").GetComponent<ComputerAI>();
	}


	void Update ()
    {
        liveEnemies = GameObject.FindObjectsOfType<EnemyAI>();
        if(liveEnemies.Length <= enemyTypes && roundRunning)
        {
            roundRunning = false;
            roundOver = true;
            comp.economicPoints += 10 + (difficulty * 10);
            //player gets points to spend based on current round and difficulty
        }
        if (roundOver)
        {
            
            //Player UI for adding too minions appear
            //Player UI for choosing role, if final wave ask if ready
            //
            
        }
        else
        {
            //hide ui and save changes
        }

        if (ready)
        {
            //minionsMade = CREATOR minions array
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                roundRunning = true;
                roundOver = false;
                ready = false;
                currentRound++;
                timer = 2f;
                enemySpawnTime = .5f;
            }
        }

        if(roundRunning)
        {
            enemySpawnTime -= Time.deltaTime;
            if (enemySpawnTime <= 0)
            {
                for (int i = 0; i <= 5; i++)
                {
                    foreach (GameObject gm in minionsMade)
                    {
                        Instantiate(gm, enemySpawn.position, Quaternion.identity);
                    }
                }
                enemySpawnTime = 3f;
            }
        }

    }
}
