using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EntityGenerator : MonoSingleton<EntityGenerator>
{
    public bool Enabled { get; set; }
    public ObservableInt distance;
    public List<SpawnData> entities = new List<SpawnData>();

    public float spawnQuantity;
    public float spawnQuantityLinearDecrement;
    public float spawnQuantityMin;
    public float spawnFrequency;
    public float spawnFrequencyLinearDecrement;
    public float spawnFrequencyMin;
    public float spawnHorizon;

    private Camera _camera;
    private bool _isSpawning = false;
    private float _screenSize;
    private void OnValidate()
    {
        if (spawnFrequency <= 0)
        {
            spawnFrequency = 0.1f;
        }
    }

    void Start()
    {
        _camera = Camera.main;
        var originScreenToWorldPoint = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.farClipPlane / 2));
        var screenWidthScreenToWorldPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, _camera.farClipPlane / 2));
        _screenSize = screenWidthScreenToWorldPoint.x - originScreenToWorldPoint.x;
        Enabled = false;
    }

    void Update()
    {
        if (!Enabled) return;

        if(_isSpawning) return;
        StartCoroutine(SpawnEntities());

    }

    private IEnumerator SpawnEntities()
    {
        _isSpawning = true;
        
        // select spawn points
        IEnumerable<Vector2> samples = new PoissonDiscSampler(_screenSize, 100, 
                DecreaseByDistance(spawnQuantity, spawnQuantityLinearDecrement, distance.Value, spawnQuantityMin, spawnQuantity))
            .Samples();
        foreach (Vector2 sample in samples)
        {
            // move sample position back to the horizon 
            yield return GenerateEntity(sample + new Vector2(-_screenSize/2, spawnHorizon));
        }

        // change spawn frequency
        yield return new WaitForSeconds(DecreaseByDistance(spawnFrequency, spawnFrequencyLinearDecrement, distance.Value, spawnFrequencyMin, spawnFrequency));
        _isSpawning = false;
    }

    private float DecreaseByDistance(float value, float decrement, float dist, float min, float max)
    {
        return Mathf.Clamp(value - dist/1000 * decrement, min, max);
    }

    private GameObject GenerateEntity(Vector2 pos)
    {
        GameObject go = Instantiate(PickRandomEntity(entities).prefab, new Vector3(pos.x, 0, pos.y), quaternion.identity);
        ScrollingPlane.Instance.Spawn(go);
        return go;
    }

    private EntityPrototype PickRandomEntity(List<SpawnData> spawnData)
    {
        List<SpawnData> prototypes = spawnData.Where(sd => distance.Value >= sd.minDistance).ToList();
        
        List<int> prob = new List<int>();
        for (int i = 0; i < prototypes.Count; i++)
        {
            for (int j = 0; j < prototypes[i].weight; j++)
            {
                prob.Add(i);
            }
        }
        return prototypes[prob[Random.Range(0, prob.Count)]].prototype;
    }
}