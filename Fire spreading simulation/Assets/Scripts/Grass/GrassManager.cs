﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : Singleton<GrassManager> {

    public List<GameObject> grassList = new List<GameObject>();
    public GameObject glassObj;
    public float grassSpawnMaximum;

    [Header("Timer")]
    public float burningSpeedSecond;
    public float burntDuration;
    public float delayDestroy;

    [Header("Color")]
    public Color defaultColor;
    public Color fireColor;
    public Color burntColor;

    public void ClearAllGrass()
    {
        foreach (var item in grassList)
        {
            Destroy(item);
        }
        grassList.Clear();
    }


    public void StopSimulation()
    {
        Grass grass = null;
        foreach (var item in grassList)
        {
            grass = item.GetComponent<Grass>();
            grass.StopAllCoroutines();
        }
    }

    public void RandomFirePropagation()
    {

    }
}
