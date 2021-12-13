using UnityEngine;

class LifeCollectable : Collectable
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
        if (PlayerShip.playerData.lives.Value < PlayerShip.maxLives)
        {
            Debug.LogFormat("Life {0} + {1}", PlayerShip.playerData.lives.Value, amount);
            PlayerShip.playerData.lives.Value += amount;
        }
        else
        {
            Debug.Log("Life at max");
        }
    }
}