using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollectable : Collectable
{
    public float duration;
    
    public override void Collect(PlayerShip ship)
    {
        base.Collect(ship);
        base.StartCollection();
        CollectionEffect();
    }

    protected override void CollectionEffect()
    {
        base.CollectionEffect();
        Debug.LogFormat("Shield booster activated for {0} seconds", duration);
        PlayerShip.GiveInvulnerability(duration);
    }
}
