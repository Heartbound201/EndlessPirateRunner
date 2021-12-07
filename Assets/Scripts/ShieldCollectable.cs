using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollectable : Collectable
{
    public float duration;
    public override void Collect(PlayerShip ship)
    {
        base.Collect(ship);
        Debug.LogFormat("Shield booster activated for {0} seconds", duration);
        ship.GiveInvulnerability(duration);
        // TODO animate
        GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
    }
}
