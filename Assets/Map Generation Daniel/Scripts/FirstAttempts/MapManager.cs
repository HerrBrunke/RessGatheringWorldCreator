using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {
#region Properties
    Map currentMap;
    public GameObject mapTileCube;
    public static MapManager Instance;
    MapGenerator mapGenerator;

    [Range(1,32)]
    public float tileSize;

    [Range(8,64)]
    public float tileHeightFactor = 16f;

    public int citiesToplace = 4;
    public int cityAmount;

    public int cityDistFromBorder = 6;
    public int cityDist = 10;
    public Vector2 lastCityPos;
    public bool placeCities = false;
    #endregion
    public List<Airport> airports = new List<Airport>();
    public List<City> cities = new List<City>();
    //public event System.Action<Map> OnMapGenerated;
    public LocalNavMeshBuilder localNavMeshBuilder;

    void Awake () {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        mapGenerator = GameObject.Find("[MapGenerator]").GetComponent<MapGenerator>();
	}

    public void CreateMapFromMapData(Map map)
    {
        //GameObject.Find("AirplaneCamera").transform.parent = null;
        cityAmount = 0;
        lastCityPos = Vector2.one;
        currentMap = map;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        cities.Clear();
        airports.Clear();
        Debug.Log("createmap from map data" + map.Width + " h: " + map.Height);
        Transform newTile;
        int materialIndex=0;
        for (int x = 0; x < map.Width; x++)
        {
            for (int z = 0; z < map.Height; z++)
            {

                materialIndex = GetRegionIndexFromHeight(map.NoiseMap[x, z]);
                
                //newTile = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
                //Destroy(newTile.GetComponent<Collider>());
                newTile = Instantiate(mapTileCube).transform;
                newTile.GetComponent<MeshRenderer>().sharedMaterial = map.Materials[materialIndex];
                //newTile.localScale = new Vector3(tileSize, map.NoiseMap[x, z] * tileSize * tileHeightFactor +0.001f, tileSize);
                newTile.localScale = new Vector3(tileSize, tileSize * materialIndex*0.25f + 0.001f, tileSize);
                newTile.position = Vector3.right * x * tileSize + Vector3.up * newTile.localScale.y*0.5f + Vector3.forward * z * tileSize;
                newTile.parent = transform;
                //If we got grass
                if(materialIndex == 3 )//|| materialIndex == 4)
                {
                    newTile.gameObject.AddComponent<NavMeshSourceTag>();
                    //newTile.gameObject.AddComponent<NavMeshL>
                }
                //Trees
                else if (materialIndex == 4)
                {
                    if (Random.Range(0, 10) > 6)
                    {
                        PlaceTree(new Vector3(x * tileSize, newTile.localScale.y, z * tileSize));
                    }
                }
                if(placeCities)
                {
                    if (cityAmount < citiesToplace
                    && materialIndex == 3
                    && x >= lastCityPos.x + cityDist
                    && z >= lastCityPos.y + cityDist
                    && x > cityDistFromBorder
                    && x < map.Width - cityDistFromBorder
                    && z > cityDistFromBorder
                    && z < map.Height - cityDistFromBorder)
                    {
                        if (CheckNeighbourRegionIndex(map, x, z, 3))
                        {
                            PlaceCity(x, newTile.localScale.y, z);
                        }
                    }
                }
            }
        }
        if (!placeCities)
        {
            //update position to center
            transform.position -= map.Width * tileSize * 0.5f * Vector3.right + map.Height * tileSize * 0.5f * Vector3.forward;

            //update localNavMeshBuilder
            localNavMeshBuilder.m_Size = new Vector3(map.Width * tileSize, 20, map.Height * tileSize);

            PlaceTownCenter();
        }
        else
        {
            //spawn airplanes
            for (int i = 0; i < cityAmount; i++)
            {
                GameObject airplane = Instantiate(Airplane, new Vector3(Random.Range(map.Width / 2, map.Width) * tileSize, 20f, Random.Range(map.Height / 2, map.Height) * tileSize), Quaternion.identity, transform);
                airplane.transform.localScale *= tileSize;
                airplane.AddComponent<AirplaneController>();
            }

            //connect cities
            if (cityAmount >= 2)
            {
                for (int i = 1; i < cities.ToArray().Length; i++)
                {
                    Debug.Log("connect");
                    ConnectCities(cities.ToArray()[i - 1], cities.ToArray()[i]);
                }
            }

        }
    }
    int GetRegionIndexFromHeight(float height)
    {
        for (int i = 0; i < mapGenerator.regions.Length; i++)
        {
            if (height <= mapGenerator.regions[i].height)
            {
                return i;
            }
        }
        return 0;
    }
    int CheckAverageNeighbourRegionIndex(Map map, int posX, int posZ)
    {
        int averageIndex = 0;
        int tilesChecked = 0;
        for (int z = posZ - 1; z <= posZ + 1; z++)
        {
            if (z >= 0 && z < map.Height)
            {
                for (int x = posX - 1; x <= posX + 1; x++)
                {
                    if (x >= 0 && x < map.Width)
                    {
                        averageIndex += GetRegionIndexFromHeight(map.NoiseMap[x, z]);
                        tilesChecked++;
                    }
                    else
                        return 0;
                }
            }
            else
                return 0;
        }
        Debug.Log(averageIndex / tilesChecked);
        return averageIndex / tilesChecked;
    }
    bool CheckNeighbourRegionIndex(Map map, int posX, int posZ, int index)
    {
        for(int z=posZ-1; z<=posZ+1; z++)
        {
            if(z>=0 && z <map.Height)
            {
                for (int x = posX - 1; x <= posX + 1; x++)
                {
                    if (x >= 0 && x < map.Width)
                    {
                        if (GetRegionIndexFromHeight(map.NoiseMap[x, z]) != index)
                            return false;
                    }
                    else
                        return false;
                }
            }
                else
                    return false;
        }
        return true;
    }
    public GameObject treePrefab;
    void PlaceTree(Vector3 position)
    {
        GameObject tree = Instantiate(treePrefab, position, Quaternion.identity, transform);
        tree.transform.localScale *= tileSize;
    }
    void PlaceTownCenter()
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 townPosition = new Vector3(0,0,0);
        Vector3 newResourcePosition = new Vector3(0, 0, 0);

        NavMesh.CalculatePath(townPosition, newResourcePosition, NavMesh.AllAreas, path);
    }
    public GameObject[] CityHouse;
    public GameObject RoadPrefab;
    public GameObject Airplane;
    void PlaceCity(int x,float y, int z)
    {
        lastCityPos = new Vector2(x, z);
        cityAmount++;
        for(int oZ=z-1; oZ<=z+1; oZ++)
        {
            for(int oX=x-1; oX<=x+1; oX++)
            {
                if (Random.Range(0, 10) > 7)
                    continue;
                Transform newHouse = Instantiate(CityHouse[0], new Vector3(oX * tileSize, y, oZ * tileSize), Quaternion.identity, transform).transform;
                newHouse.localScale = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.75f, 3f), Random.Range(0.5f, 1.5f))*tileSize;
            }

        }
        for (float oZ = z - 1.25f; oZ <= z + 1.5f; oZ+=2.25f)
        {
            for (float oX = x - 1.25f; oX <= x + 1.5f; oX+=0.25f)
            {
                Transform newRoad = Instantiate(RoadPrefab, new Vector3(oX * tileSize, y+0.001f, oZ * tileSize), Quaternion.identity,transform).transform;
                newRoad.localScale *=  tileSize;
                //newRoad = Instantiate(RoadPrefab, new Vector3(oX * tileSize, y + 0.001f, oZ * tileSize), Quaternion.identity, transform).transform;
                //newRoad.localScale *= tileSize;
            }

        }
        airports.Add(new Airport(new Vector3(x * tileSize, y, z * tileSize)));

        cities.Add(new City(new Vector2Int(x,z),new Vector3(x * tileSize,y,z*tileSize)));

        //Instantiate(CityHouse[0], new Vector3((x-1) * tileSize, y, z * tileSize), Quaternion.identity);

        //Instantiate(CityHouse[0], new Vector3((x + 1) * tileSize, y, z * tileSize), Quaternion.identity);

    }
    float GetTileHeight(int x, int y)
    {
        return currentMap.NoiseMap[x, y] * tileSize * tileHeightFactor + 0.001f;
    }
    void ConnectCities(City From, City To)
    {
        Vector2Int roadPlacePosition = From.mapPosition;
        while (roadPlacePosition != To.mapPosition)//(roadPlacePosition - To.mapPosition).magnitude < 4)
        {
            float distanceX = Mathf.Abs(roadPlacePosition.x - To.mapPosition.x);
            float distanceY = Mathf.Abs(roadPlacePosition.y - To.mapPosition.y);
            Debug.Log("distanceX:" + distanceX + " distanceY:" + distanceY);
            //int direction;
            //decide direction
            if (distanceX > distanceY)
            {
                if (roadPlacePosition.x > To.mapPosition.x)
                {
                    //direction = -1;

                    for (int i = roadPlacePosition.x; i > To.mapPosition.x; i--)
                    {
                        Transform newRoad = Instantiate(RoadPrefab, new Vector3(i * tileSize, GetTileHeight(i,roadPlacePosition.y) + 0.005f, roadPlacePosition.y * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                        newRoad = Instantiate(RoadPrefab, new Vector3(i * tileSize - 0.25f * tileSize, GetTileHeight(i, roadPlacePosition.y) + 0.005f, roadPlacePosition.y * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                    }
                    roadPlacePosition.x = To.mapPosition.x;
                }
                else
                {
                    //direction = 1;

                    for (int i = roadPlacePosition.x; i < To.mapPosition.x; i++)
                    {
                        Transform newRoad = Instantiate(RoadPrefab, new Vector3(i * tileSize, GetTileHeight(i, roadPlacePosition.y) + 0.005f, roadPlacePosition.y * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                        newRoad = Instantiate(RoadPrefab, new Vector3(i * tileSize + 0.25f * tileSize, GetTileHeight(i, roadPlacePosition.y) + 0.005f, roadPlacePosition.y * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                    }
                    roadPlacePosition.x = To.mapPosition.x;
                }

            }
            else
            {
                if (roadPlacePosition.y > To.mapPosition.y)
                {
                    //direction = -1;

                    for (int i = roadPlacePosition.y; i > To.mapPosition.y; i--)
                    {
                        Transform newRoad = Instantiate(RoadPrefab, new Vector3(roadPlacePosition.x * tileSize, GetTileHeight(roadPlacePosition.x, i) + 0.005f, i * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                        newRoad = Instantiate(RoadPrefab, new Vector3(roadPlacePosition.x * tileSize, GetTileHeight(roadPlacePosition.x, i) + 0.005f, i * tileSize - 0.25f * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                    }
                    roadPlacePosition.y = To.mapPosition.y;
                }
                else
                {
                    //direction = 1;

                    for (int i = roadPlacePosition.x; i < To.mapPosition.x; i++)
                    {
                        Transform newRoad = Instantiate(RoadPrefab, new Vector3(roadPlacePosition.x * tileSize, GetTileHeight(roadPlacePosition.x, i) + 0.005f, i * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                        newRoad = Instantiate(RoadPrefab, new Vector3(roadPlacePosition.x * tileSize, GetTileHeight(roadPlacePosition.x, i) + 0.005f, i * tileSize + 0.25f * tileSize), Quaternion.identity, transform).transform;
                        newRoad.localScale *= tileSize;
                    }
                    roadPlacePosition.y = To.mapPosition.y;
                }

            }
        }
    }
}
public class Road
{

}
public class City
{
    public Vector2Int mapPosition;
    public Vector3 position;
    public List<Road> roads;
    public City(Vector2Int mapPos, Vector3 pos)
    {
        mapPosition = mapPos;
        position = pos;
    }
}
public class Airport
{

    public Vector3 location;
    public Airport(Vector3 position)
    {
        location = position;
    }
}

