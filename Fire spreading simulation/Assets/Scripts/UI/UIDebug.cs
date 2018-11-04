using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDebug : MonoBehaviour
{
    [SerializeField] private Button m_GenerateBtn;
    [SerializeField] private Button m_ClearBtn;
    [SerializeField] private Button m_PlayBtn;
    [SerializeField] private Button m_StopBtn;
    [SerializeField] private Button m_FireBtn;
    [SerializeField] private Toggle m_Add;
    [SerializeField] private Toggle m_Remove;
    [SerializeField] private Toggle m_Fire;
    [SerializeField] private Slider m_WindSpread;
    [SerializeField] private Slider m_WindDirection;
    [SerializeField] private Button m_QuitBtn;

    void Start()
    {
        m_GenerateBtn.onClick.AddListener(()=>
        {
            TerrainManager.Instance.GenerateObjectOnTerrain();
        });
        m_ClearBtn.onClick.AddListener(()=>
        {
            GrassManager.Instance.ClearAllGrass();
        });
        m_PlayBtn.onClick.AddListener(()=>
        {

        });
        m_StopBtn.onClick.AddListener(()=>
        {
            GrassManager.Instance.StopSimulation();
        });
        m_FireBtn.onClick.AddListener(()=>
        {
            GrassManager.Instance.RandomFirePropagation();
        });
        m_QuitBtn.onClick.AddListener(()=>
        {
            GameManager.Instance.Quit();
        });
    }
}