using System;
using UnityEngine;

public class EnemyShip : Enemy
{
    public int lives;
    
    public CannonSystem cannonSystem;
    public DropSystem dropSystem;

    [Header("Animation")] 
    public string sinkAnim;
    
    private PlayerShip _playerShip;
    private Animator _animator;
    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!_playerShip) return;
        
        if(!cannonSystem) return;
        
        bool isInRange = Vector3.Distance(transform.position, _playerShip.transform.position) < cannonSystem.targetIndicatorRadius;
        bool isBehindPlayer = _playerShip.transform.position.z > transform.position.z;
        
        if (!isInRange || isBehindPlayer || cannonSystem.IsObstructed(_playerShip.transform.position)) 
            return;
        
        cannonSystem.Fire(_playerShip.transform.position + _playerShip.rigidbody.velocity);
    }

    public override void GetHit(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            _animator.Play(sinkAnim);
        }
    }

    public void Sink()
    {
        dropSystem.DropReward();
        GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
    }

    

    

}