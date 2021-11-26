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
    public Transform environment;
    public ObservableInt score;
    public List<SpawnData<EntityPrototype>> entities = new List<SpawnData<EntityPrototype>>();

    public float quantityMin;
    public float quantityMax;
    public float quantity;
    public float quantityLinearChange;
    
    public float frequencyMin;
    public float frequencyMax;
    public float frequency;
    public float frequencyLinearChange;

    private Camera _camera;
    private bool _isSpawning = false;
    private float _screenSize;

    void Start()
    {
        _camera = Camera.main;
        var originScreenToWorldPoint = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.farClipPlane / 2));
        var screenWidthScreenToWorldPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, _camera.farClipPlane / 2));
        _screenSize = (screenWidthScreenToWorldPoint.x - originScreenToWorldPoint.x) * 3;
        Enabled = false;
    }

    void Update()
    {
        if (!Enabled) return;

        Generate();

    }

    public void Generate()
    {
        if(_isSpawning) return;
        StartCoroutine(DoGenerate());
    }

    private IEnumerator DoGenerate()
    {
        _isSpawning = true;
        
        // select spawn points
        IEnumerable<Vector2> samples = new PoissonDiscSampler(_screenSize, 100, 
                ChangeByDistance(quantity, quantityLinearChange, score.Value, quantityMin, quantityMax))
            .Samples();
        foreach (Vector2 sample in samples)
        {
            // move sample position back to the horizon 
            yield return GenerateEntity(sample + new Vector2(-_screenSize/2 + transform.position.x, transform.position.z));
        }

        // change spawn frequency
        yield return new WaitForSeconds(ChangeByDistance(frequency, frequencyLinearChange, score.Value, frequencyMin, frequencyMax));
        _isSpawning = false;
    }

    private float ChangeByDistance(float value, float decrement, float dist, float min, float max)
    {
        return Mathf.Clamp(value + dist * decrement, min, max);
    }

    private GameObject GenerateEntity(Vector2 pos)
    {
        GameObject go = Instantiate(PickRandomEntity(entities).prefab, new Vector3(pos.x, 0, pos.y), quaternion.identity);
        go.transform.SetParent(environment);
        return go;
    }

    private EntityPrototype PickRandomEntity(List<SpawnData<EntityPrototype>> spawnData)
    {
        List<SpawnData<EntityPrototype>> prototypes = spawnData.Where(sd => score.Value >= sd.minScore).ToList();
        
        List<int> prob = new List<int>();
        for (int i = 0; i < prototypes.Count; i++)
        {
            for (int j = 0; j < prototypes[i].weight; j++)
            {
                prob.Add(i);
            }
        }
        if (prob.Count == 0) return null;
        return prototypes[prob[Random.Range(0, prob.Count)]].data;
    }
}