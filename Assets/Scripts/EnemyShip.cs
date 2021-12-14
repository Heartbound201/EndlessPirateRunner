using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : Enemy
{
    public int lives;
    
    public CannonSystem cannonSystem;
    public DropSystem dropSystem;

    [Header("Animation")] 
    public string sinkAnim;
    public string idleAnim;
    
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
        
        if (lives > 0 && (!isInRange || isBehindPlayer || cannonSystem.IsObstructed(_playerShip.transform.position))) 
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
        _animator.Play(idleAnim);
        dropSystem.DropReward();
        GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
    }

    

    

}