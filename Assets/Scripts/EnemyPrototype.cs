using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Prototype/Enemy Ship")]
public class EnemyPrototype : EntityPrototype
{
    [Header("Ship")] public int lives;

    [Header("Drop")] public List<Drop> drops = new List<Drop>();

    public override GameObject Build(GameObject obj)
    {
        EnemyShip enemyShipComponent = obj.GetComponent<EnemyShip>();
        enemyShipComponent.lives = lives;
        enemyShipComponent.dropSystem.drops = drops;
        return obj;
    }
}