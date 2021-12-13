using UnityEngine;

class GoldCollectable : Collectable
{
    public int amount;
    
    public override void Collect(PlayerShip ship)
    {
        base.Collect(ship);
        base.StartCollection();
        CollectionEffect();
    }

    protected override void CollectionEffect()
    {
        base.CollectionEffect();
        Debug.LogFormat("Gold {0} + {1}", PlayerShip.playerData.gold.Value, amount);
        PlayerShip.playerData.gold.Value += amount;
    }
}