using UnityEngine;

class LifeCollectable : Collectable
{
    public int amount;
    public override void Collect(PlayerShip ship)
    {
        base.Collect(ship);
        if (ship.playerData.lives.Value < ship.maxLives)
        {
            Debug.LogFormat("Life {0} + {1}", ship.playerData.lives.Value, amount);
            ship.playerData.lives.Value += amount;
        }
        else
        {
            Debug.Log("Life at max");
        }

        // TODO animation
        GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
    }
}