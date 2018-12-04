using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClass {

    public Transform location;
    public string type;
    public int level;

    public TowerClass(Transform plotSpot, string t, int lvl)
    {
        location = plotSpot;
        type = t;
        level = lvl;
    }
}
