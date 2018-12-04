using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    ComputerAI comp;

    public int difficulty = 0;
    public int currentRound = 1;
    float timer = 2f;

    [SerializeField] Transform enemySpawn;
    EnemyAI[] liveEnemies;
    List<GameObject> minionsMade = new List<GameObject>();

    [SerializeField] GameObject spot1;
    [SerializeField] GameObject spot2;
    [SerializeField] GameObject spot3;

    float enemySpawnTime = 1f;
    int groupsSpawned = 0;
    public int groupsPerWave = 3;

    public bool ready = false;
    public bool roundOver = false;
    public bool roundRunning = false;


    void Start()
    {
        comp = GameObject.Find("Computer").GetComponent<ComputerAI>();
    }


    void Update()
    {
        liveEnemies = GameObject.FindObjectsOfType<EnemyAI>();
        if (liveEnemies.Length <= 3 && roundRunning && groupsSpawned >= groupsPerWave)
        {
            roundRunning = false;
            ready = false;
            roundOver = true;
            comp.health += 10 + (10 * difficulty);
            comp.economicPoints += 10 + (difficulty * 10);
            //player gets points to spend based on current round and difficulty
        }
        if (roundOver)
        {

            //Player UI for adding to minions appear
            //Player UI for choosing role, if final wave ask if ready
            //

        }
        else
        {
            //hide ui and save changes
        }

        if (ready)
        {
            minionsMade.Clear();
            if(spot1.GetComponentInChildren<EnemyAI>())
            {
                minionsMade.Add(spot1.GetComponentInChildren<EnemyAI>().gameObject);
            }
            if (spot2.GetComponentInChildren<EnemyAI>())
            {
                minionsMade.Add(spot2.GetComponentInChildren<EnemyAI>().gameObject);
            }
            if (spot3.GetComponentInChildren<EnemyAI>())
            {
                minionsMade.Add(spot3.GetComponentInChildren<EnemyAI>().gameObject);
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                roundRunning = true;
                roundOver = false;
                ready = false;
                currentRound++;
                timer = 1f;
                enemySpawnTime = 0f;
                groupsSpawned = 0;
            }
        }
        if (roundRunning)
        {
            enemySpawnTime -= Time.deltaTime;
            if (enemySpawnTime <= 0 && groupsSpawned < groupsPerWave)
            {
                foreach (GameObject gm in minionsMade)
                {

                    Instantiate(gm, enemySpawn.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 3f)), Quaternion.identity);
                }
                enemySpawnTime = 1.5f;
                groupsSpawned++;
            }
        }
    }
}
