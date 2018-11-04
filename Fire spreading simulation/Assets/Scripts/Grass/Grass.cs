using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GrassState.State currentState;
    private GrassManager grassManager;
    private UIDebug uIDebug;
    [SerializeField] private MeshCollider m_Collider;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private Transform m_ChildTransform;

    [Header("Delay timer")]
    private WaitForSeconds burntDuration;
    private WaitForSeconds delayDestroy;

    void Start()
    {
        grassManager    = GrassManager.Instance;
        uIDebug         = UIDebug.Instance;
        burntDuration   = new WaitForSeconds(GrassManager.Instance.burntDuration);
        delayDestroy    = new WaitForSeconds(GrassManager.Instance.delayDestroy);

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
                    StartCoroutine("OnFirePropagation");
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
        SetColor(grassManager.defaultColor);
        yield return null;
    }

    IEnumerator OnFirePropagation()
    {
        yield return new WaitForSeconds(uIDebug.windSpreadValue.value);
        SetColor(grassManager.fireColor);
        Search();
        SwichState(GrassState.State.Burnt);
    }
    IEnumerator OnBurnt()
    {
        yield return burntDuration;
        SetColor(grassManager.burntColor);
        yield return delayDestroy;
        grassManager.grassList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    #endregion

    // Enable cone collider
    void Search()
    {
        m_Collider.enabled = true;
    }

    public void SetColor(Color _color)
    {
        m_MeshRenderer.material.color =  _color;
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
        if (_other.gameObject.tag == "Fireable" && currentState == GrassState.State.Normal)
        {
            SwichState(GrassState.State.Fire);
        }
    }
}