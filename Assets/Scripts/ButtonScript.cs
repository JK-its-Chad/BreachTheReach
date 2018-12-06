using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    [SerializeField] TurnManager bigBoss;
    [SerializeField] UpgradeMinions minMan;
    public string func = "";

    public void Click()
    {
        if(bigBoss && (func == "ReadyUp" || 
            func == "playSup" || func == "playAtck" || 
            func == "diffUP" || func == "diffDOWN" || 
            func == "groupUP" || func == "groupDOWN"))
        {
            bigBoss.Invoke(func, 0);
        }
        else
        {
            Debug.Log(bigBoss.name);
            Debug.Log(func);
        }

        if(minMan && (func == "hpUP" || func == "hpDown" || 
            func == "dmgUP" || func == "dmgDown" || 
            func == "spdUP" || func == "spdDown" ||
            func == "atckspdUP" || func == "atckspdDown" ||
            func == "SwapDodge" || func == "SwapIgnore" || func == "SwapTower"))
        {
            minMan.Invoke(func, 0);
        }
        else
        {
            Debug.Log(minMan.name);
            Debug.Log(func);
        }


    }
}
