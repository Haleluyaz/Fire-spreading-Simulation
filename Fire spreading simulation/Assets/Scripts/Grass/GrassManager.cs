using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : Singleton<GrassManager> {

    [HideInInspector] public List<GameObject> grassList = new List<GameObject>();
    public GameObject glassObj;
    public float grassSpawnMaximum;
    [SerializeField] private int randomCount;

    [Header("Timer")]
    public float burntDuration;
    public float delayDestroy;

    [Header("Color")]
    public Color defaultColor;
    public Color fireColor;
    public Color burntColor;

    public void ClearAllGrass()
    {
        if(grassList.Count > 0)
        {
            foreach (var item in grassList)
            {
                Destroy(item);
            }
            grassList.Clear();
        }
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
        int rand = 0;
        for (int i = 0; i < randomCount; i++)
        {
            rand = Random.Range(0, grassList.Count);
            grassList[rand].GetComponent<Grass>().SwichState(GrassState.State.Fire);
        }
    }
}
