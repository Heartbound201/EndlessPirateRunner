using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CannonSystem : MonoBehaviour
{
    public List<Cannon> cannonBattery = new List<Cannon>();
    public float firingCooldown;
    public GameObject targetIndicatorPrefab;
    public float targetIndicatorSpeed;
    public float targetIndicatorRadius;
    public LaunchArcRenderer arcRenderer;
    public LayerMask layerMask;

    private GameObject _targetIndicator;
    private bool _isFiring;
    private LineRenderer _lr;
    private int _dotsNumber = 100;
    private float _dotSpacing = .1f;
    private Vector3 _pos;
    private float _timeStamp;

    private bool CanShoot => cannonBattery.Count > 0 && !_isFiring;

    private void Start()
    {
        _targetIndicator = Instantiate(targetIndicatorPrefab, transform);
        // _lr = GetComponent<LineRenderer>();
        // _lr.positionCount = _dotsNumber;
        ResetTargetIndicator();
    }

    public void UpdateTrajectory(Vector3 cannonPos, Vector3 forceApplied)
    {
        List<Vector3> linePoints = new List<Vector3> {cannonPos};
        _timeStamp = _dotSpacing;
        for (int i = 0; i < _dotsNumber; i++)
        {
            _pos.x = (cannonPos.x + forceApplied.x * _timeStamp);
            _pos.y = (cannonPos.y + forceApplied.y * _timeStamp) -
                     (Physics2D.gravity.magnitude * _timeStamp * _timeStamp) / 2f;
            _pos.z = (cannonPos.z + forceApplied.z * _timeStamp);

            linePoints.Add(_pos);
            _timeStamp += _dotSpacing;
        }

        // _lr.SetPositions(linePoints.ToArray());
        // _lr.enabled = true;
    }

    public void AimAt(Vector3 target)
    {
        if (cannonBattery.Count == 0) return;

        Cannon cannon = SelectClosestCannon(target);
        
        _targetIndicator.SetActive(true);
        _targetIndicator.transform.Translate(target.x * targetIndicatorSpeed, 0,
            target.z * targetIndicatorSpeed);

        Vector3 targetPosition = _targetIndicator.transform.position;
        
        Vector3 centerPosition = cannon.transform.position;
        float distance = Vector3.Distance(targetPosition, centerPosition);
 
        if (distance > targetIndicatorRadius)
        {
            Vector3 fromOriginToObject = targetPosition - centerPosition;
            fromOriginToObject *= targetIndicatorRadius / distance; 
            targetPosition = centerPosition + fromOriginToObject;
        }
        
        _targetIndicator.transform.position = targetPosition;

        // simulate arc
        // UpdateTrajectory(cannon.transform.position, cannon.BallisticVelocity(clampedPosition, cannon.firingAngle));
        // arcRenderer.RenderArc(100, 10);
    }

    private Cannon SelectClosestCannon(Vector3 target)
    {
        if (cannonBattery.Count == 0) return null;
        float minDistance = 0f;
        Cannon retCannon = null;
        for (int i = 0; i < cannonBattery.Count; i++)
        {
            float distance = Vector3.Distance(target, cannonBattery[i].transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                retCannon = cannonBattery[i];
            }
            if (i == 0)
            {
                minDistance = distance;
                retCannon = cannonBattery[i];
            }
        }
        foreach (Cannon cannon in cannonBattery)
        {
            
            float distance = Vector3.Distance(target, cannon.transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                retCannon = cannon;
            }
        }

        return retCannon;
    }

    private IEnumerator DoFire(Vector3 target)
    {
        _isFiring = true;
        Cannon cannon = SelectClosestCannon(target);
        cannon.Fire(target);
        yield return new WaitForSeconds(firingCooldown);
        _isFiring = false;
    }

    private IEnumerator DoFire()
    {
        _isFiring = true;
        Cannon cannon = SelectClosestCannon(_targetIndicator.transform.position);
        cannon.Fire(_targetIndicator.transform.position);
        yield return new WaitForSeconds(firingCooldown);
        _isFiring = false;
    }

    public void Fire()
    {
        // _lr.enabled = false;
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

    public bool IsObstructed(Vector3 target)
    {
        Cannon cannon = SelectClosestCannon(target);
        if (Physics.Raycast(cannon.transform.position, (target - cannon.transform.position).normalized,
            out RaycastHit hit, targetIndicatorRadius, layerMask))
        {
            Debug.DrawRay(cannon.transform.position, target - cannon.transform.position, Color.yellow, 3f);
            Debug.Log("Cannon Obstructed from " + hit.collider.name);
            if (hit.collider.GetComponent<PlayerShip>())
            {
                return false;
            }

            return true;
        }

        return false;
    }
}