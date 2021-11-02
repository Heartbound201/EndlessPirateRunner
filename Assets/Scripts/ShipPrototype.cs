using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ship", menuName = "Create Ship")]
public class ShipPrototype : ScriptableObject
{
    public GameObject prefab;
    public int cost;
    public Sprite sprite;
    public bool hasCannons;
}
