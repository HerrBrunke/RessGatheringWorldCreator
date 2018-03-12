using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {

    int planetWidth = 32;
    int planetHeight = 32;
    int octaves = 4;
    float noiseScale = 12.5f;
    float persistance = 0.368f;
    float lacunarity = 1.8f;
    int seed = 21;
    Vector2 offset = new Vector2(0, 0);
    public Transform planet;

    //we want to create a six sided planet
    void GeneratePlanet()
    {

        for(int i=0; i<6; i++)
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(planetWidth, planetHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        }


    }
    
}

