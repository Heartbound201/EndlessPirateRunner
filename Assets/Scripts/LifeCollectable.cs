using UnityEngine;

class LifeCollectable : Collectable
{
    public ObservableInt life;
    public int amount;
    public override void Collect()
    {
        base.Collect();
        life.Value += amount;
        Debug.LogFormat("Life {0} + {1}", life.Value, amount);
        // TODO animation
        Destroy(gameObject);
    }
}