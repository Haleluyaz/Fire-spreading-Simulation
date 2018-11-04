using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    [SerializeField] private GameObject m_SpawnObject;
    [SerializeField] private Collider m_Terrain;
    void Update()
    {
        // Left click
        OnMouseClick(0);
    }

    void OnMouseClick(int _index)
    {
        if (Input.GetMouseButtonDown(_index))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (m_Terrain.Raycast(ray, out hit, Mathf.Infinity))
            {
                m_SpawnObject.transform.position = hit.point;
            }
        }
    }
}
