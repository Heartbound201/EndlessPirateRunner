using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollectable : Collectable
{
    public float duration;
    public float sizeMultiplier;
    public override void Collect(PlayerShip ship)
    {
        base.Collect(ship);
        base.StartCollection();
        CollectionEffect();
    }

    protected override void CollectionEffect()
    {
        base.CollectionEffect();
        Debug.LogFormat("Magnet booster activated for {0} seconds with size {1}", duration, sizeMultiplier);
        PlayerShip.collectorSystem.ChangeSize(sizeMultiplier, duration);
    }
}
