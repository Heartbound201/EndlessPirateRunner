using System;
using UnityEngine.Serialization;

[Serializable]
public class SpawnData<T>
{
    public T data;
    public int minScore;
    public int weight;

}