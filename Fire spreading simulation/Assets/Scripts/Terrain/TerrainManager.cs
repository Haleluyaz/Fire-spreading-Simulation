using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : Singleton<TerrainManager>
{
    private GrassManager m_GrassManager;
    [SerializeField] private Terrain m_Terrain;
    [SerializeField] private float m_YOffset = 0.5f;

    private float m_TerrainWidth;
    private float m_TerrainLength;
    private float m_TerrainPosX;
    private float m_TerrainPosZ;

    void Start()
    {
        m_GrassManager  = GrassManager.Instance;
        m_TerrainWidth  = m_Terrain.terrainData.size.x;
        m_TerrainLength = m_Terrain.terrainData.size.z;

        m_TerrainPosX   = m_Terrain.transform.position.x;
        m_TerrainPosZ   = m_Terrain.transform.position.z;

        // GenerateObjectOnTerrain();
    }

    public void GenerateObjectOnTerrain()
    {
        float randX     = 0f;
        float randZ     = 0f;
        float yVal      = 0f;
        GameObject grassObj = null;
		for (int i = 0; i < m_GrassManager.grassSpawnMaximum; i++)
		{
			//Generate random x,z,y position on the terrain
			randX   = UnityEngine.Random.Range(m_TerrainPosX, m_TerrainPosX + m_TerrainWidth);
			randZ   = UnityEngine.Random.Range(m_TerrainPosZ, m_TerrainPosZ + m_TerrainLength);
			yVal    = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));

			//Apply Offset if needed
			yVal = yVal + m_YOffset;
			grassObj = (GameObject)Instantiate(m_GrassManager.glassObj, 
                                                new Vector3(randX, yVal, randZ), 
                                                Quaternion.identity);
            m_GrassManager.grassList.Add(grassObj);
		}
    }
}