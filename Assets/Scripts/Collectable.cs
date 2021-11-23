using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Collectable : Entity
{
    public AudioClipSO collectionSfx;
    public virtual void Collect(PlayerShip ship)
    {
        Debug.Log("Collecting " + name);
        AudioManager.Instance.PlaySFX(collectionSfx);
    }
    
}