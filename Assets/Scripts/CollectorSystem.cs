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

    private IEnumerator DoChangeSize(float size, float duration)
    {
        _boxCollider.size = _startingSize * size;
        yield return new WaitForSeconds(duration);
        _boxCollider.size = _startingSize;
    }

}