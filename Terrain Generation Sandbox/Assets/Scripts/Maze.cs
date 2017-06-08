using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public GameObject V;

    Terrain terrain;
    UnityEngine.TerrainData tData;

    int xRes;
    int yRes;

    float[,] heights;

    void Start()
    {
        terrain = transform.GetComponent<Terrain>();
        tData = terrain.terrainData;

        xRes = tData.heightmapWidth;
        yRes = tData.heightmapHeight;

        for(int x = 0; x <= 100; ++x)
        {
            for(int z = 1; z <= 100; ++z)
            {
                if(Random.Range(0, 100) < 30)
                Instantiate(V, new Vector3(x*2 + 1, 0, z*2 + 1), Quaternion.identity);
            }
        }
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
                heights[x, y] = Mathf.PerlinNoise(x * 0.25f * strength, y * 0.25f * strength) * 0.1f;
        //heights[x, y] = Random.Range(0.0f, strength) * 0.5f;
        tData.SetHeights(0, 0, heights);
    }

    void resetPoints()
    {
        heights = tData.GetHeights(0, 0, xRes, yRes);
        for (int y = 0; y < yRes; ++y)
            for (int x = 0; x < xRes; ++x)
                heights[x, y] = 0;
        tData.SetHeights(0, 0, heights);
    }
}
