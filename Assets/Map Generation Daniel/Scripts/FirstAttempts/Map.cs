using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public float[,] NoiseMap { get; private set; }
    public Color[] ColorMap { get; private set; }
    public Material[] Materials { get; private set; }

    public Map(int width, int height, float[,] noiseMap, Color[] colorMap, List<Material> materials)
    {
        Width = width;
        Height = height;
        NoiseMap = noiseMap;
        ColorMap = colorMap;
        Materials = materials.ToArray();
    }
}