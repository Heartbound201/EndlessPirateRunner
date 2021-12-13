using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Collectable : Entity
{
    public AudioClipSO collectionSfx;
    public string gatherAnim;
    
    protected PlayerShip PlayerShip;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void Collect(PlayerShip ship)
    {
        PlayerShip = ship;
    }

    protected virtual void StartCollection()
    {
        Debug.Log("Collecting " + name);
        AudioManager.Instance.PlaySFX(collectionSfx);
        _animator.Play(gatherAnim);
        StartCoroutine(MoveToPlayer());
    }

    public virtual void EndCollection()
    {
        Debug.Log("end collection");
        GameObjectPoolController.Enqueue(GetComponent<Poolable>());
    }

    protected virtual void CollectionEffect()
    {
        
    }

    protected IEnumerator MoveToPlayer()
    {
        float elapsedTime = 0;
        float waitTime = 1f;
        Vector3 currentPos = transform.position;
        while (elapsedTime < waitTime)
        {
            transform.position = Vector3.Lerp(currentPos, PlayerShip.transform.position, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
 
            yield return null;
        }  
        transform.position = PlayerShip.transform.position;
        yield return null;
    }
}