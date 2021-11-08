using UnityEngine;

class GoldCollectable : Collectable
{
    public ObservableInt gold;
    public int amount;
    public override void Collect()
    {
        base.Collect();
        gold.Value += amount;
        Debug.LogFormat("Gold{0} + {1}", gold.Value, amount);
        // TODO animate
        Destroy(gameObject);
    }
}