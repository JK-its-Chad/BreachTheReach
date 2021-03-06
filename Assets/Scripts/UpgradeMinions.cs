﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMinions : MonoBehaviour {

    [SerializeField] EnemyAI prefab;
    [SerializeField] PlayerAI play;

    [SerializeField] TextMesh hp;
    [SerializeField] TextMesh dmg;
    [SerializeField] TextMesh spd;
    [SerializeField] TextMesh atckspd;
    [SerializeField] TextMesh dodge;
    [SerializeField] TextMesh ignore;
    [SerializeField] TextMesh tower;


    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        hp.text = prefab.health.ToString();
        dmg.text = prefab.damage.ToString();
        spd.text = prefab.speed.ToString();
        atckspd.text = prefab.attackTimer.ToString("F2");

        if(prefab.dodgeArrows) { dodge.text = "!"; }
        else { dodge.text = ""; }

        if (prefab.ignoreTarget) { ignore.text = "!"; }
        else { ignore.text = ""; }

        if (prefab.attackTower) { tower.text = "!"; }
        else { tower.text = ""; }
    }

    public void hpUP()
    {
        if(play.points >= 10)
        {
            prefab.health += 25;
            play.points -= 10;
        }
    }
    public void hpDown()
    {
        if (prefab.health >= 25)
        {
            prefab.health -= 25;
            play.points += 10;
        }
    }

    public void dmgUP()
    {
        if (play.points >= 10)
        {
            prefab.damage += 5;
            play.points -= 10;
        }
    }
    public void dmgDown()
    {
        if (prefab.damage >= 5)
        {
            prefab.damage -= 5;
            play.points += 10;
        }
    }

    public void spdUP()
    {
        if (play.points >= 5)
        {
            prefab.speed += 1;
            play.points -= 5;
        }
    }
    public void spdDown()
    {
        if (prefab.speed > 1)
        {
            prefab.speed -= 1;
            play.points += 5;
        }
    }

    public void atckspdUP()
    {
        if (play.points >= 10 && prefab.attackTimer > .1f)
        {
            prefab.attackTimer -= .05f;
            play.points -= 10;
        }
    }
    public void atckspdDown()
    {
        if (prefab.attackTimer < 1)
        {
            prefab.attackTimer += .05f;
            play.points += 10;
        }
    }

    public void SwapDodge()
    {
        if(!prefab.dodgeArrows && play.points >= 25)
        {
            prefab.dodgeArrows = true;
            play.points -= 25;
        }
        else if(prefab.dodgeArrows)
        {
            prefab.dodgeArrows = false;
            play.points += 25;
        }
    }
    public void SwapIgnore()
    {
        if (!prefab.ignoreTarget && play.points >= 25)
        {
            prefab.ignoreTarget = true;
            play.points -= 25;
        }
        else if (prefab.ignoreTarget)
        {
            prefab.ignoreTarget = false;
            play.points += 25;
        }
    }
    public void SwapTower()
    {
        if (!prefab.attackTower && play.points >= 25)
        {
            prefab.attackTower = true;
            play.points -= 25;
        }
        else if (prefab.attackTower)
        {
            prefab.attackTower = false;
            play.points += 25;
        }
    }
}
