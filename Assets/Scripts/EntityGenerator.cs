using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGenerator : MonoBehaviour
{
    public Transform parent;
    public float horizonDistance = 200f;
    public float horizonHeight = 0f;
    public List<CollectablePrototype> CollectablePrototypes = new List<CollectablePrototype>();
    public List<ObstaclePrototype> ObstaclePrototypes = new List<ObstaclePrototype>();
    public List<EnemyPrototype> EnemyPrototypes = new List<EnemyPrototype>();
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void GenerateEnemy()
    {
        EnemyPrototype randomProto = EnemyPrototypes[Random.Range(0, EnemyPrototypes.Count)];
        GameObject o = Instantiate(randomProto.prefab, parent);
        var screenPosition = GetRandomScreenPosition();
        o.transform.position = new Vector3(screenPosition.x, horizonHeight, horizonDistance);
    }

    void GenerateObstacle()
    {
        ObstaclePrototype randomProto = ObstaclePrototypes[Random.Range(0, ObstaclePrototypes.Count)];
        GameObject o = Instantiate(randomProto.prefab, parent.position, GetRandomYRotation());
        o.transform.SetParent(parent);
        var screenPosition = GetRandomScreenPosition();
        o.transform.position = new Vector3(screenPosition.x, horizonHeight, horizonDistance);
    }

    void GenerateCollectable()
    {
        CollectablePrototype randomProto = CollectablePrototypes[Random.Range(0, CollectablePrototypes.Count)];
        GameObject o = Instantiate(randomProto.prefab, parent.position, GetRandomYRotation());
        o.transform.SetParent(parent);
        var screenPosition = GetRandomScreenPosition();
        o.transform.position = new Vector3(screenPosition.x, horizonHeight, horizonDistance);
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
}
