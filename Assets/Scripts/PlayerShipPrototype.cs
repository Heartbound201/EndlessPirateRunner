using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Prototype/Player Ship")]
public class PlayerShipPrototype : EntityPrototype
{
    public int cost;
    public Sprite sprite;
    public bool hasCannons;
}
