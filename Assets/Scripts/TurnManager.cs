using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    ComputerAI comp;
    PlayerAI play;
    Player player;

    [SerializeField] TextMesh readyText;
    [SerializeField] TextMesh timerText;

    [SerializeField] TextMesh pChoiceS;
    [SerializeField] TextMesh pChoiceA;

    [SerializeField] TextMesh waveNumber;
    [SerializeField] TextMesh difficultyNumber;
    [SerializeField] TextMesh groupsNumber;

    public int difficulty = 0;
    public int currentRound = 1;
    float timer = 3.00f;

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

    public bool playerSupport = true;
    public bool playerAttack = false;


    void Start()
    {
        comp = GameObject.Find("Computer").GetComponent<ComputerAI>();
        play = GameObject.Find("PlayerUI").GetComponent<PlayerAI>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update()
    {
        timerText.text = ((int)timer).ToString();
        waveNumber.text = currentRound.ToString();
        difficultyNumber.text = difficulty.ToString();
        groupsNumber.text = groupsPerWave.ToString();

        if(playerAttack && pChoiceA.text != "!")
        {
            pChoiceA.text = "!";
            pChoiceS.text = "";
        }
        if (playerSupport && pChoiceS.text != "!")
        {
            pChoiceA.text = "";
            pChoiceS.text = "!";
        }


        liveEnemies = GameObject.FindObjectsOfType<EnemyAI>();
        if (liveEnemies.Length <= 3 && roundRunning && groupsSpawned >= groupsPerWave)
        {
            roundRunning = false;
            ready = false;
            readyText.text = "";
            roundOver = true;
            currentRound++;
            groupsPerWave++;

            if(comp.health <= 990 + (10 * difficulty))
            {
                comp.health += 10 + (10 * difficulty);
            }
            comp.economicPoints += 10 + (difficulty * 10);

            play.points += (currentRound * 10) + (difficulty * 5);
            play.points += 1000 - comp.health;
            play.OpenUI();

            player.RoundOver();
        }

        if (ready && roundOver)
        {
            play.CloseUI();
            readyText.text = "!";
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
                readyText.text = "";
                timer = 3.00f;
                enemySpawnTime = 0f;
                groupsSpawned = 0;
                
                if(playerAttack)
                {
                    player.PlayAttack();
                }
                if(playerSupport)
                {
                    player.PlaySupport();
                }
            }
        }
        else if(!ready && roundOver)
        {
            timer = 3.00f;
            readyText.text = "";
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

    public void ReadyUP()
    {
        timer = 3.00f;
        if (!ready && roundOver)
        {
            ready = true;
        }
        else if(ready && roundOver)
        {
            ready = false;
        }
    }

    public void playSup()
    {
        if(!playerSupport && roundOver)
        {
            playerSupport = true;
            playerAttack = false;
        }
    }
    public void playAtck()
    {
        if (!playerAttack && roundOver)
        {
            playerSupport = false;
            playerAttack = true;
        }
    }

    public void diffUP()
    {
        if (roundOver)
        {
            difficulty++;
        }
    }
    public void diffDOWN()
    {
        if(difficulty > 0 && roundOver)
        {
            difficulty--;
        }
    }

    public void groupUP()
    {
        if(play.points >= 10 && roundOver)
        {
            play.points -= 10;
            groupsPerWave++;
        }
    }
    public void groupDOWN()
    {
        if (groupsPerWave > 1 && roundOver)
        {
            play.points += 10;
            groupsPerWave--;
        }
    }
}
