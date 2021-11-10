using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityGenerator : MonoSingleton<EntityGenerator>
{
    public Transform parent;
    public float horizonDistance = 200f;
    public float horizonHeight = 0f;
    public List<CollectablePrototype> CollectablePrototypes = new List<CollectablePrototype>();
    public List<ObstaclePrototype> ObstaclePrototypes = new List<ObstaclePrototype>();
    public List<EnemyPrototype> EnemyPrototypes = new List<EnemyPrototype>();
    public bool Enabled { get; set; }
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        Enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Enabled) return;
        
        if (Random.value < .001f)
        {
            GenerateCollectable();
        }

        if (Random.value < .001f)
        {
            GenerateObstacle();
        }

        if (Random.value < .001f)
        {
            GenerateEnemy();
        }
    }

    private EntityPrototype PickRandomEntity(List<EntityPrototype> prototypes)
    {
        return prototypes[Random.Range(0, prototypes.Count)];
    }

    private void GenerateEnemy()
    {
        EnemyPrototype randomProto = EnemyPrototypes[Random.Range(0, EnemyPrototypes.Count)];
        GameObject o = Instantiate(randomProto.prefab, parent);
        var screenPosition = GetRandomScreenPosition();
        o.transform.position = new Vector3(screenPosition.x, horizonHeight, horizonDistance);
        EnemyShip enemyShip = o.GetComponent<EnemyShip>();
        ScrollingPlane.Instance.Spawn(o);
    }

    void GenerateObstacle()
    {
        ObstaclePrototype randomProto = ObstaclePrototypes[Random.Range(0, ObstaclePrototypes.Count)];
        GameObject o = Instantiate(randomProto.prefab, parent.position, GetRandomYRotation());
        o.transform.SetParent(parent);
        var screenPosition = GetRandomScreenPosition();
        o.transform.position = new Vector3(screenPosition.x, horizonHeight, horizonDistance);
        ScrollingPlane.Instance.Spawn(o);
    }

    void GenerateCollectable()
    {
        CollectablePrototype randomProto = CollectablePrototypes[Random.Range(0, CollectablePrototypes.Count)];
        GameObject o = Instantiate(randomProto.prefab, parent.position, GetRandomYRotation());
        o.transform.SetParent(parent);
        var screenPosition = GetRandomScreenPosition();
        o.transform.position = new Vector3(screenPosition.x, horizonHeight, horizonDistance);
        ScrollingPlane.Instance.Spawn(o);
    }

    private Quaternion GetRandomYRotation()
    {
        return Quaternion.Euler(new Vector3(0, 0, 0));
    }

    private Vector3 GetRandomScreenPosition()
    {
        return _camera.ScreenToWorldPoint(
            new Vector3(Random.Range(0, Screen.width), 0, _camera.farClipPlane / 2));
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private float levelScale;
    [SerializeField] private float neighborRadius;
    [SerializeField] private GameObject treePrefab;

    public float[,] GeneratePerlinNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ, Wave[] waves)
    {
        // create an empty noise map with the mapDepth and mapWidth coordinates
        float[,] noiseMap = new float[mapDepth, mapWidth];
        for (int zIndex = 0; zIndex < mapDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                // calculate sample indices based on the coordinates, the scale and the offset
                float sampleX = (xIndex + offsetX) / scale;
                float sampleZ = (zIndex + offsetZ) / scale;
                float noise = 0f;
                float normalization = 0f;
                foreach (Wave wave in waves)
                {
                    // generate noise value using PerlinNoise for a given Wave
                    noise += wave.amplitude * Mathf.PerlinNoise(sampleX * wave.frequency + wave.seed,
                        sampleZ * wave.frequency + wave.seed);
                    normalization += wave.amplitude;
                }

                // normalize the noise value so that it is within 0 and 1
                noise /= normalization;
                noiseMap[zIndex, xIndex] = noise;
            }
        }

        return noiseMap;
    }

    public void GenerateTrees(int levelDepth, int levelWidth, float levelScale)
    {
        // generate a tree noise map using Perlin Noise
        float[,] treeMap = this.GeneratePerlinNoiseMap(levelDepth, levelWidth, levelScale, 0, 0, this.waves);
        for (int zIndex = 0; zIndex < levelDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < levelWidth; xIndex++)
            {
                float treeValue = treeMap[zIndex, xIndex];
                // compares the current tree noise value to the neighbor ones
                int neighborZBegin = (int) Mathf.Max(0, zIndex - this.neighborRadius);
                int neighborZEnd = (int) Mathf.Min(levelDepth - 1, zIndex + this.neighborRadius);
                int neighborXBegin = (int) Mathf.Max(0, xIndex - this.neighborRadius);
                int neighborXEnd = (int) Mathf.Min(levelWidth - 1, xIndex + this.neighborRadius);
                float maxValue = 0f;
                for (int neighborZ = neighborZBegin; neighborZ <= neighborZEnd; neighborZ++)
                {
                    for (int neighborX = neighborXBegin; neighborX <= neighborXEnd; neighborX++)
                    {
                        float neighborValue = treeMap[neighborZ, neighborX];
                        // saves the maximum tree noise value in the radius
                        if (neighborValue >= maxValue)
                        {
                            maxValue = neighborValue;
                        }
                    }
                }

                // if the current tree noise value is the maximum one, place a tree in this location
                if (treeValue == maxValue)
                {
                    Vector3 treePosition = new Vector3(xIndex, 0, zIndex);
                    GameObject tree = Instantiate(this.treePrefab, treePosition, Quaternion.identity) as GameObject;
                    tree.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                }
            }
        }
    }
}

[Serializable]
public class Wave
{
    public float seed;
    public float frequency;
    public float amplitude;
}