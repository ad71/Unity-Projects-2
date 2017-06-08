using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainData : MonoBehaviour {

    Terrain terrain;
    UnityEngine.TerrainData tData;
    public GameObject cylinder;

    int xRes;
    int yRes;

    float[,] heights;

	void Start () {
        terrain = transform.GetComponent<Terrain>();
        tData = terrain.terrainData;

        xRes = tData.heightmapWidth;
        yRes = tData.heightmapHeight;

        Instantiate(cylinder, new Vector3(1, 0, 1), Quaternion.identity);
	}

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 25), "Wrinkle"))
            randomizePoints(0.1f);

        if (GUI.Button(new Rect(10, 40, 100, 25), "Reset"))
            resetPoints();
    }

    void randomizePoints(float strength)
    {
        heights = tData.GetHeights(0, 0, xRes, yRes);
        for (int y = 0; y < yRes; ++y)
            for (int x = 0; x < xRes; ++x)
            {
                //if(x % 100 == 0)
                  //  Instantiate(cylinder, new Vector3(x, y, 0), Quaternion.identity);
                    //heights[x, y] = strength;
                heights[x, y] = 0;
            }
                //heights[x, y] = Mathf.PerlinNoise(x * 0.25f * strength, y * 0.25f * strength) * 0.1f;
                //heights[x, y] = Random.Range(0.0f, strength) * 0.5f;
        tData.SetHeights(0, 0, heights);
    }

    void resetPoints()
    {
        heights = tData.GetHeights(0, 0, xRes, yRes);
        for(int y = 0; y < yRes; ++y)
            for(int x = 0; x < xRes; ++x)
                heights[x, y] = 0;
        tData.SetHeights(0, 0, heights);
    }
}
