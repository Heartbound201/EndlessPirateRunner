using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Prototype/Collectable")]
public class CollectablePrototype : EntityPrototype
{
    public override GameObject Build(GameObject obj)
    {
        return obj;
    }
}