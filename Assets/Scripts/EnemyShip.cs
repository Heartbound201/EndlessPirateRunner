using System;
using UnityEngine;

public class EnemyShip : Enemy
{
    public int lives;
    
    public CannonSystem cannonSystem;
    public DropSystem dropSystem;

    private PlayerShip _playerShip;
    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
    }

    private void Update()
    {
        if(!_playerShip) return;
        
        if(!cannonSystem) return;
        
        bool isInRange = Vector3.Distance(transform.position, _playerShip.transform.position) < cannonSystem.targetIndicatorRadius;
        bool isBehindPlayer = _playerShip.transform.position.z > transform.position.z;
        
        if (!isInRange || isBehindPlayer || cannonSystem.IsObstructed(_playerShip.transform.position)) 
            return;
        
        cannonSystem.Fire(_playerShip.transform.position + Vector3.forward * _playerShip.forwardSpeed);
    }

    public override void GetHit(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            // TODO sunk animation
            dropSystem.DropReward();
            GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
        }
    }

    

    

}