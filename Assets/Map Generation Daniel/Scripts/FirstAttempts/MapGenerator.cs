using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=WP-Bm65Q-1Y
public class MapGenerator : MonoBehaviour {

    

    public enum DrawMode
    {
        NoiseMap,ColorMap
    }
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;

    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    List<Material> materials;

    public GameObject VillagerAI;
    private void Awake()
    {
        if(VillagerAI != null)
            VillagerAI.SetActive(false);
    }
    private void Start()
    {

        materials = new List<Material>();
        Shader standardShader = Shader.Find("Unlit/Color");
        foreach (TerrainType t in regions)
        {
            Material newMat = new Material(standardShader);
            newMat.color = t.color;
            materials.Add(newMat);
        }

        //first generate the map
        GenerateMap();
        //then active the villager AI

        VillagerAI.SetActive(true);
       // VillagerAI.transform.position = Vector3.up * 14;
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        float currentHeight;
        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y ++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                currentHeight = noiseMap[x, y];
                for(int i=0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();//GameObject.Find("MapDisplay").GetComponent<MapDisplay>();
        if(display != null)
        {
            if (drawMode == DrawMode.NoiseMap)
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
            else if (drawMode == DrawMode.ColorMap)
                display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
        MapManager mapManager = GameObject.Find("[Map]").GetComponent<MapManager>();
        if (mapManager != null)
        {
            Map map = new Map(mapWidth, mapHeight, noiseMap, colorMap, materials);
            mapManager.CreateMapFromMapData(map);
        }
    }

    private void OnValidate()
    {
        if(mapWidth < 1)
            mapWidth = 1;

        if(mapHeight < 1)
            mapHeight = 1;
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
