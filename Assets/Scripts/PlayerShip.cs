using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : Ship
{
    public UnityAction OnFatalHit;
    
    public ObservableInt gold;
    public Cannon cannon;
    public int firingCooldown = 1;
    public GameObject targetIndicatorPrefab;
    public float targetIndicatorMovingSpeed = 5f;
    public Bounds targetIndicatorBounds;
    private GameObject _targetIndicator;
    private bool _isFiring = false;

    private bool CanShoot => !(cannon == null) && !_isFiring;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _targetIndicator = Instantiate(targetIndicatorPrefab, transform);
    }

    void Update()
    {
        var axisHorCannon = CrossPlatformInputManager.GetAxis("HorizontalCannon");
        var axisVerCannon = CrossPlatformInputManager.GetAxis("VerticalCannon");

        if (CrossPlatformInputManager.GetButton("Cannon"))
        {
            _targetIndicator.SetActive(true);
            _targetIndicator.transform.Translate(axisHorCannon * targetIndicatorMovingSpeed, 0, axisVerCannon * targetIndicatorMovingSpeed);
            
            Vector3 clampedPosition = _targetIndicator.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -targetIndicatorBounds.extents.x, targetIndicatorBounds.extents.x);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, 0, targetIndicatorBounds.extents.z);
            _targetIndicator.transform.position = clampedPosition;
            
        }
        else
        {
            if (_targetIndicator.transform.position.z >= cannon.transform.position.z)
            {
                StartCoroutine(Fire(_targetIndicator.transform.position));
            }
            _targetIndicator.transform.position = transform.position;
            _targetIndicator.SetActive(false);
        }

    }

    private IEnumerator Fire(Vector3 target)
    {
        _isFiring = true;
        cannon.Fire(target);
        yield return new WaitForSeconds(firingCooldown);
        _isFiring = false;
    }

    private void FireRaycast(Vector3 touch)
    {
        Ray raycast = _camera.ScreenPointToRay(touch);
        Debug.DrawRay(raycast.origin, raycast.direction * 100);
        if (Physics.Raycast(raycast, out var raycastHit))
        {
            Debug.Log("Something Hit " + raycastHit.collider.name);
            if (raycastHit.collider != null) ;
            {
                EnemyShip enemyShip = raycastHit.collider.gameObject.GetComponent<EnemyShip>();
                if (enemyShip != null)
                {
                    if (CanShoot)
                    {
                        cannon.Fire(enemyShip.transform.position);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Obstacle>())
        {
            GetHit();
        }
        else if (obj.GetComponent<EnemyShip>())
        {
            GetHit();
        }
        else if(other.gameObject.GetComponent<Collectable>())
        {
            Collectable collectable = obj.GetComponent<Collectable>();
            gold.Value += collectable.Worth;
            Destroy(obj);
            Debug.LogFormat("Collected {0} gold. {1} total", collectable.Worth, gold);
        }
    }

    public override void GetHit()
    {
        lives--;
        if (lives <= 0)
        {
            // TODO sunk animation
            OnFatalHit?.Invoke();
            Destroy(gameObject);
        }
    }

}
