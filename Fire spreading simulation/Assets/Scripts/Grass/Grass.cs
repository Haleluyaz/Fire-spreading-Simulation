using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GrassState.State currentState;
    private GrassManager grassManager;
    [SerializeField] private MeshCollider m_Collider;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    WaitForSeconds delayToFire;
    WaitForSeconds burningSpeed;
    WaitForSeconds delayBurnt;
    WaitForSeconds delayDestroy;

    void Start()
    {
        grassManager = GrassManager.Instance;
        delayToFire = new WaitForSeconds(GrassManager.Instance.delayToFire);
        burningSpeed = new WaitForSeconds(GrassManager.Instance.burningSpeed);
        delayBurnt = new WaitForSeconds(GrassManager.Instance.delayBurnt);
        delayDestroy = new WaitForSeconds(GrassManager.Instance.delayDestroy);

        SwichState(GrassState.State.Normal);
    }

    public void UpdateState(GrassState.State _state)
    {
        switch (_state)
        {
            case GrassState.State.None: Debug.Log("Fix it dude"); break;
            case GrassState.State.Normal:
                {
                    StartCoroutine("OnNormal");
                }
                break;
            case GrassState.State.Fire:
                {
                    StartCoroutine("OnFire");
                }
                break;
            case GrassState.State.Burnt:
                {
                    StartCoroutine("OnBurnt");
                }
                break;
        }
    }

    #region  Coroutine

    IEnumerator OnNormal()
    {
        SetColor(currentState);
        yield return null;
    }

    IEnumerator OnFire()
    {
        yield return burningSpeed;
        SetColor(currentState);
        yield return StartCoroutine("Search");
        SwichState(GrassState.State.Burnt);
    }
    IEnumerator OnBurnt()
    {
        yield return delayBurnt;
        SetColor(currentState);
        yield return delayDestroy;
        Destroy(this.gameObject);
    }

    #endregion

    IEnumerator Search()
    {
        m_Collider.enabled = true;
        yield return null;
        // m_Collider.enabled = false;
    }

    public void SetColor(GrassState.State _state)
    {
        switch (_state)
        {
            case GrassState.State.None: Debug.Log("Fix it dude"); break;
            case GrassState.State.Normal:
                {
                    m_MeshRenderer.material.color =  grassManager.defaultColor;
                }
                break;
            case GrassState.State.Fire:
                {
                    m_MeshRenderer.material.color = grassManager.fireColor;
                }
                break;
            case GrassState.State.Burnt:
                {
                    m_MeshRenderer.material.color = grassManager.burntColor;
                }
                break;
        }
    }


    void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Fireable")
        {
            Debug.Log("GG");
            Grass otherGrass = _other.GetComponent<Grass>();
            if(otherGrass.currentState == GrassState.State.Normal)
            {
                SwichState(GrassState.State.Fire);
            }
        }
    }

    public void SwichState(GrassState.State _state)
    {
        currentState = _state;
        UpdateState(currentState);
    }
}