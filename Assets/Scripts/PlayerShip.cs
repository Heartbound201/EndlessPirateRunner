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
    
    public int maxLives;
    public float graceTime;
    public int scoreIncreasedPerSecond;

    public float lateralSpeedMin;
    public float lateralSpeedMax;
    public float lateralSpeed;
    public float lateralSpeedLinearChange;
    
    public float forwardSpeedMin;
    public float forwardSpeedMax;
    public float forwardSpeed;
    public float forwardSpeedLinearChange;

    public CannonSystem cannonSystem;
    public CollectorSystem collectorSystem;
    public Rigidbody rigidbody;

    [Header("Animation")] 
    public string hitAnim;
    public string invulAnim;
    public string sinkAnim;
    
    private bool _canMove = false;
    private bool _isInvulnerable = false;
    private bool _isInGrace = false;
    private bool _isIncrementingScore = false;
    private Animator _animator;
    

    private void Start()
    {
        collectorSystem.ship = this;
        _animator = GetComponent<Animator>();
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
            if (CrossPlatformInputManager.GetButtonUp("Cannon") && playerData.lives.Value > 0)
            {
                cannonSystem.Fire();
            }
        }
       
    }

    private void FixedUpdate()
    {
        Move();
        
        // aim cannon
        if (cannonSystem)
        {
            var axisHorCannon = CrossPlatformInputManager.GetAxis("HorizontalCannon");
            var axisVerCannon = CrossPlatformInputManager.GetAxis("VerticalCannon");
            if (CrossPlatformInputManager.GetButton("Cannon"))
            {
                cannonSystem.AimAt(new Vector3(axisHorCannon, 0, axisVerCannon));
            }
        }
    }

    private void Move()
    {
        if(!_canMove) return;
        var axis = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector3 forwardVelocity = Vector3.forward * ChangeByDistance(forwardSpeed, forwardSpeedLinearChange, playerData.score.Value, forwardSpeedMin, forwardSpeedMax);
        Vector3 lateralVelocity = Vector3.right * axis * ChangeByDistance(lateralSpeed, lateralSpeedLinearChange, playerData.score.Value, lateralSpeedMin, lateralSpeedMax);
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
        if(_isInvulnerable)
        {
            EnemyShip damageable = other.gameObject.GetComponent<EnemyShip>();
            if (damageable != null)
            {
                damageable.GetHit(1);
            }
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
        if(_isInvulnerable || _isInGrace) return;
        playerData.lives.Value -= damage;
        StartCoroutine(FlashRed());
        if (playerData.lives.Value <= 0)
        {
            _canMove = false;
            rigidbody.velocity = Vector3.zero;
            _animator.Play(sinkAnim);
        }
    }

    public void Sink()
    {
        OnFatalHit?.Invoke();
        Destroy(gameObject);
    }
    
    private float ChangeByDistance(float value, float decrement, float dist, float min, float max)
    {
        return Mathf.Clamp(value + dist * decrement, min, max);
    }

    private IEnumerator FlashRed()
    {
        _isInGrace = true;
        _animator.Play(hitAnim);
        yield return new WaitForSeconds(graceTime);
        if (playerData.lives.Value > 0)
        {
            _animator.Play("default");
        }
        _isInGrace = false;
    }
    
    public void GiveInvulnerability(float duration)
    {
        StartCoroutine(DoGiveInvulnerability(duration));
    }

    private IEnumerator DoGiveInvulnerability(float duration)
    {
        _isInvulnerable = true;
        _animator.Play(invulAnim);
        yield return new WaitForSeconds(duration);
        _animator.Play("default");
        _isInvulnerable = false;
    }
}