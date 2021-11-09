using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(LineRenderer))]
public class CannonSystem : MonoBehaviour
{
    public Cannon cannon;
    public float firingCooldown = 1f;
    public GameObject targetIndicatorPrefab;
    public float targetIndicatorMovingSpeed = 5f;
    public Bounds targetIndicatorBounds;

    private GameObject _targetIndicator;
    private bool _isFiring;

    private LineRenderer _lr;
    private int _dotsNumber = 100;
    private float _dotSpacing = .1f;
    private Vector3 _pos;
    private float _timeStamp;
    private bool CanShoot => !(cannon == null) && !_isFiring && _targetIndicator.transform.position.z >= cannon.transform.position.z;
    
    private void Start()
    {
        _targetIndicator = Instantiate(targetIndicatorPrefab, transform);
        _lr = GetComponent<LineRenderer>();
        _lr.positionCount = _dotsNumber;
        ResetTargetIndicator();
    }
    
    public void UpdateTrajectory (Vector3 cannonPos, Vector3 forceApplied)
    {
        List<Vector3> linePoints = new List<Vector3> {cannonPos};
        _timeStamp = _dotSpacing;
        for (int i = 0; i < _dotsNumber; i++) {
            _pos.x = (cannonPos.x + forceApplied.x * _timeStamp);
            _pos.y = (cannonPos.y + forceApplied.y * _timeStamp) - (Physics2D.gravity.magnitude * _timeStamp * _timeStamp) / 2f;
            _pos.z = (cannonPos.z + forceApplied.z * _timeStamp);
            
            linePoints.Add(_pos);
            _timeStamp += _dotSpacing;
        }

        _lr.SetPositions(linePoints.ToArray());
        _lr.enabled = true;
    }

    public void AimAt(Vector3 target)
    {
        if(cannon == null) return;
        
        _targetIndicator.SetActive(true);
        _targetIndicator.transform.Translate(target.x * targetIndicatorMovingSpeed, 0, target.z * targetIndicatorMovingSpeed);
            
        Vector3 clampedPosition = _targetIndicator.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -targetIndicatorBounds.extents.x, targetIndicatorBounds.extents.x);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, 0, targetIndicatorBounds.extents.z);
        _targetIndicator.transform.position = clampedPosition;
        
        // simulate arc
        UpdateTrajectory(cannon.transform.position, cannon.BallisticVelocity(clampedPosition, cannon.firingAngle));
    }

    private IEnumerator DoFire(Vector3 target)
    {
        _isFiring = true;
        cannon.Fire(target);
        yield return new WaitForSeconds(firingCooldown);
        _isFiring = false;
    }

    private IEnumerator DoFire()
    {
        _isFiring = true;
        cannon.Fire(_targetIndicator.transform.position);
        yield return new WaitForSeconds(firingCooldown);
        _isFiring = false;
    }

    public void Fire()
    {
        _lr.enabled = false;
        if (CanShoot)
        {
            StartCoroutine(DoFire());
        }
        ResetTargetIndicator();
    }

    public void Fire(Vector3 target)
    {
        if (CanShoot)
        {
            StartCoroutine(DoFire(target));
        }
        ResetTargetIndicator();
    }

    private void ResetTargetIndicator()
    {
        _targetIndicator.transform.position = transform.position;
        _targetIndicator.SetActive(false);
    }
}
