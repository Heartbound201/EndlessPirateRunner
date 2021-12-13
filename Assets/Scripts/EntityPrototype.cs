using UnityEngine;

public abstract class EntityPrototype : ScriptableObject
{
    public string poolKey;
    public GameObject prefab;
    
    public abstract GameObject Build(GameObject obj);
}