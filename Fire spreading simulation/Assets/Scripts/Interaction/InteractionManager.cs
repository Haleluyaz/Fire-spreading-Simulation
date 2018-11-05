using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <typeparam name="InteractionManager"></typeparam>
public class InteractionManager : Singleton<InteractionManager>
{
    public InteractionState.State currentState;
    [SerializeField] private GameObject m_SpawnObject;
    [SerializeField] private Collider m_Terrain;
    [SerializeField] private Vector3 m_YOffset;
    private Camera m_MainCamera;
    private GrassManager grassManager;
    private UIDebug uIDebug;
    private int layerMask = 1 << 9; // "Fireable"

    void Awake()
    {
        m_MainCamera = Camera.main;
    }

    void Start()
    {
        grassManager = GrassManager.Instance;
        uIDebug = UIDebug.Instance;
    }

    void Update()
    {
        OnMouseClick();
    }

    // LMB
    void OnMouseClick()
    {
        switch (currentState)
        {
            case InteractionState.State.None: Debug.LogError("Select one dude"); break;
            case InteractionState.State.Add: Add(0); break;
            case InteractionState.State.Remove: Remove(0); break;
            case InteractionState.State.Ignite: Ignite(0); break;
        }
    }

    void Add(int _index)
    {
        if (Input.GetMouseButtonDown(_index))
        {
            RaycastHit hit;
            GameObject go = null;
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            if (m_Terrain.Raycast(ray, out hit, Mathf.Infinity))
            {
                go = Instantiate(m_SpawnObject, hit.point, Quaternion.identity);
                grassManager.grassList.Add(go);
                go.transform.position += m_YOffset;
            }
        }
    }

    void Remove(int _index)
    {
        if (Input.GetMouseButtonDown(_index))
        {
            RaycastHit hit;
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                grassManager.grassList.Remove(hit.transform.gameObject);
                Destroy(hit.transform.gameObject, 0.2f);
            }
        }
    }

    void Ignite(int _index)
    {
        if (Input.GetMouseButtonDown(_index))
        {
            RaycastHit hit;
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                hit.transform.GetComponent<Grass>().SwichState(GrassState.State.Fire);
            }
        }
    }

    // Call in UI
    public void SwichState()
    {
        currentState = (InteractionState.State)uIDebug.dropdownInteractionState.value;
    }
}