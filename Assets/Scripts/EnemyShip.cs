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
    private Vector3 _startPos;
    private Quaternion _startRot;
    
    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        _animator = GetComponent<Animator>();
        _startPos = transform.position;
        _startRot = transform.rotation;
    }

    private void Update()
    {
        if(!_playerShip) return;
        
        if(!cannonSystem) return;
        
        bool isInRange = Vector3.Distance(transform.position, _playerShip.transform.position) < cannonSystem.targetIndicatorRadius;
        bool isBehindPlayer = _playerShip.transform.position.z > transform.position.z;
        
        if (lives > 0 && (!isInRange || isBehindPlayer || cannonSystem.IsObstructed(_playerShip.transform.position))) 
            return;

        cannonSystem.Fire(_playerShip.transform.position + new Vector3(0, 0, _playerShip.rigidbody.velocity.z));
        
    }

    public override void GetHit(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            _animator.Play(sinkAnim);
            dropSystem.DropReward();
        }
    }

    public void Sink()
    {
        transform.position = _startPos;
        transform.rotation = _startRot;
        _animator.Play(idleAnim);
        GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
    }

    

    

}