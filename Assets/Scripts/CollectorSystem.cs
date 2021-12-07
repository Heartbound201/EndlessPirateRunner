using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectorSystem : MonoBehaviour
{
    public PlayerShip ship;
    
    private BoxCollider _boxCollider;
    private Vector3 _startingSize;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _startingSize = _boxCollider.size;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if(other.gameObject.GetComponent<Collectable>())
        {
            Collectable collectable = obj.GetComponent<Collectable>();
            collectable.Collect(ship);
        }
    }

    public void ChangeSize(float size, float duration)
    {
        StartCoroutine(DoChangeSize(size, duration));
    }

    private IEnumerator DoChangeSize(float sizeMultiplier, float duration)
    {
        Vector3 newSize = _boxCollider.size;
        newSize.x *= sizeMultiplier;
        _boxCollider.size = newSize;
        yield return new WaitForSeconds(duration);
        _boxCollider.size = _startingSize;
    }

}