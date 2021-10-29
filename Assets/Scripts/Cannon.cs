using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{
    // TODO change to scriptable object?
    // TODO add animator
    // TODO add vfx
    // TODO add sfx
    public GameObject cannonBallPrefab;
    public Transform firingPoint;
    [Range(10f, 45f)]
    public float firingAngle;
    public float firingSpeed;
    private Camera _camera;

    // Update is called once per frame
    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        // TODO move elsewhere
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            FireRaycast(Input.GetTouch(0).position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            FireRaycast(Input.mousePosition);
        }
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
                Enemy enemy = raycastHit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    GameObject cannonBall = Instantiate(cannonBallPrefab, firingPoint.transform.position, Quaternion.identity);
                    Vector3 aim = PredictOnMovingTarget(firingPoint.transform, enemy.transform, Vector3.back * 50f, firingSpeed);
                    // cannonBall.GetComponent<Rigidbody>().velocity = BallisticVelocity(aim, firingAngle);
                    cannonBall.GetComponent<Rigidbody>().velocity = aim;
                    cannonBall.transform.SetParent(ScrollingPlane.Instance.transform);
                    Destroy(cannonBall, 10f);
                    enemy.GetHit();
                }
            }
        }
    }
    
    private Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        Debug.LogFormat("dir {0}, dir norm {1}, vel {2}, angle {3}, target {4}, res {5}", dir, dir.normalized, velocity, a, destination, dir.normalized * velocity);
        return 100 * dir.normalized; // Return a normalized vector.
    }

    private Vector3 PredictOnMovingTarget(Transform shooter, Transform target, Vector3 velTarget, float bulletSpeed)
    {
        Vector3 relativePos = target.position - shooter.position;
        double a = Vector3.Dot(velTarget, velTarget) - (bulletSpeed * bulletSpeed);
        double b = 2 * Vector3.Dot(velTarget, relativePos);
        double c = Vector3.Dot(relativePos, relativePos);

        double disc = b * b - 4 * a * c;
        double t1 = (-b + Math.Sqrt(disc)) / (2 * a);
        double t2 = (-b - Math.Sqrt(disc)) / (2 * a);
        double t;

        if (t1 < t2 && t1 > 0)
        {
            t = t1;
        }
        else
        {
            t = t2;
        }

        Debug.LogFormat("aim at {0}, t1 {1}, t2 {2}", (target.position + velTarget * (float) t), t1, t2);
        return velTarget * (float) t + target.position;




        // Vector3 P_InitialTargetPosition = target.position;
        // Vector3 V_InitialVelocityTarget = velTarget;
        // float s_firingSpeed = firingSpeed;
        // float t_timeOfImpact = 0f;
    }
    
}