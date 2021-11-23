using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : Entity, IDamageable
{
    public UnityAction OnFatalHit;

    public PlayerData playerData;
    
    public float lateralSpeed;
    public float forwardSpeed;
    public int maxLives;
    public float graceTime;
    public int scoreIncreasedPerSecond;

    public CannonSystem cannonSystem;
    public CollectorSystem collectorSystem;
    public Renderer renderer;
    public Rigidbody rigidbody;

    private bool _canMove = false;
    private bool _isInvulnerable = false;
    private bool _isIncrementingScore = false;

    private void Start()
    {
        collectorSystem.ship = this;
    }

    private void Update()
    {
        // increment score
        if(!_isIncrementingScore)
        {
            StartCoroutine(IncrementScore());
        }
        
        // aim cannon
        if(cannonSystem)
        {
            var axisHorCannon = CrossPlatformInputManager.GetAxis("HorizontalCannon");
            var axisVerCannon = CrossPlatformInputManager.GetAxis("VerticalCannon");
            if (CrossPlatformInputManager.GetButton("Cannon"))
            {
                cannonSystem.AimAt(new Vector3(axisHorCannon, 0, axisVerCannon));
            }
            else
            {
                cannonSystem.Fire();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(!_canMove) return;
        var axis = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector3 forwardVelocity = Vector3.forward * forwardSpeed;
        Vector3 lateralVelocity = Vector3.right * axis * lateralSpeed;
        rigidbody.velocity = forwardVelocity + lateralVelocity;
    }

    public void Sail()
    {
        _canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Obstacle>())
        {
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            obstacle.Collide(this);
        }
    }
    
    private IEnumerator IncrementScore()
    {
        _isIncrementingScore = true;
        playerData.score.Value += scoreIncreasedPerSecond;
        yield return new WaitForSeconds(1);
        _isIncrementingScore = false;
    }

    public void GetHit(int damage)
    {
        if(_isInvulnerable) return;
        StartCoroutine(FlashRed());
        playerData.lives.Value -= damage;
        if (playerData.lives.Value <= 0)
        {
            // TODO sunk animation
            OnFatalHit?.Invoke();
            Destroy(gameObject);
        }


    }

    private IEnumerator FlashRed()
    {
        _isInvulnerable = true;
        var material = renderer.material;
        material.color = Color.red;
        yield return new WaitForSeconds(graceTime);
        material.color = Color.white;
        _isInvulnerable = false;
    }
}