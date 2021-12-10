using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Prototype/Obstacle")]
public class ObstaclePrototype : EntityPrototype
{
    public int damage;
    public override GameObject Build(GameObject obj)
    {
        var component = obj.GetComponent<Obstacle>();
        component.collisionDamage = damage;
        return obj;
    }
}
