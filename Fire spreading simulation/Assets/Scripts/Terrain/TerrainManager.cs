﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Glass;
    [SerializeField] private Terrain m_Terrain;
    [SerializeField] private float m_YOffset = 0.5f;

    private float m_TerrainWidth;
    private float m_TerrainLength;

    private float m_TerrainPosX;
    private float m_TerrainPosZ;


    void Start()
    {
        m_TerrainWidth = m_Terrain.terrainData.size.x;
        m_TerrainLength = m_Terrain.terrainData.size.z;

        m_TerrainPosX = m_Terrain.transform.position.x;
        m_TerrainPosZ = m_Terrain.transform.position.z;

        GenerateObjectOnTerrain();
    }

    void GenerateObjectOnTerrain()
    {

		for (int i = 0; i < 40; i++)
		{
			//Generate random x,z,y position on the terrain
			float randX = UnityEngine.Random.Range(m_TerrainPosX, m_TerrainPosX + m_TerrainWidth);
			float randZ = UnityEngine.Random.Range(m_TerrainPosZ, m_TerrainPosZ + m_TerrainLength);
			float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));

			//Apply Offset if needed
			yVal = yVal + m_YOffset;
			GameObject objInstance = (GameObject)Instantiate(m_Glass, new Vector3(randX, yVal, randZ), Quaternion.identity);
		}
    }
}