using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GrassState.State currentState;
    private GrassManager grassManager;
    [SerializeField] private MeshCollider m_Collider;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private Transform m_ChildTransform;

    [Header("Delay timer")]
    private WaitForSeconds burningSpeed;
    private WaitForSeconds delayBurnt;
    private WaitForSeconds delayDestroy;

    void Start()
    {
        grassManager    = GrassManager.Instance;
        burningSpeed    = new WaitForSeconds(GrassManager.Instance.burningSpeedSecond);
        delayBurnt      = new WaitForSeconds(GrassManager.Instance.burntDuration);
        delayDestroy    = new WaitForSeconds(GrassManager.Instance.delayDestroy);

        SwichState(GrassState.State.Normal);
    }

    void LateUpdate()
    {
        RotateCollider();
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
        Search();
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

    // Search with cone collider
    void Search()
    {
        m_Collider.enabled = true;
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

    public void RotateCollider()
    {
        m_ChildTransform.localEulerAngles = WindZoneManager.Instance.windZoneTrans.localEulerAngles;
    }
    public void SwichState(GrassState.State _state)
    {
        currentState = _state;
        UpdateState(currentState);
    }

    void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Fireable")
        {
            Grass otherGrass = _other.GetComponent<Grass>();
            if(otherGrass.currentState == GrassState.State.Normal)
            {
                otherGrass.SwichState(GrassState.State.Fire);
            }
        }
    }
}