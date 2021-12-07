using UnityEngine;

class GoldCollectable : Collectable
{
    public int amount;
    public override void Collect(PlayerShip ship)
    {
        base.Collect(ship);
        Debug.LogFormat("Gold {0} + {1}", ship.playerData.gold.Value, amount);
        ship.playerData.gold.Value += amount;
        // TODO animate
        GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
    }
}