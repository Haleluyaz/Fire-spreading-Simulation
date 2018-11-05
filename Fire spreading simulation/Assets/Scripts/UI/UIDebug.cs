using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDebug : Singleton<UIDebug>
{
    [SerializeField] private Button m_GenerateBtn;
    [SerializeField] private Button m_ClearBtn;
    [SerializeField] private Button m_StopBtn;
    [SerializeField] private Button m_FireBtn;
    public Dropdown dropdownInteractionState;
    public Slider windSpreadValue;
    public Slider windDirection;
    [SerializeField] private Button m_QuitBtn;

    void Start()
    {
        m_GenerateBtn.onClick.AddListener(() =>
        {
            TerrainManager.Instance.GenerateObjectOnTerrain();
        });
        m_ClearBtn.onClick.AddListener(() =>
        {
            GrassManager.Instance.ClearAllGrass();
        });
        m_StopBtn.onClick.AddListener(() =>
        {
            GrassManager.Instance.StopSimulation();
        });
        m_FireBtn.onClick.AddListener(() =>
        {
            GrassManager.Instance.RandomFirePropagation();
        });
        m_QuitBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.Quit();
        });
    }
}