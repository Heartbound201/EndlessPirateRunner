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
    [Range(10f, 45f)]
    public float firingAngle;
    public float firingSpeed;
    public ParticleSystem firingVfx;


    public void Fire(Vector3 target, Vector3 targetVel)
    {
        Vector3 position = transform.position;
        Vector3 aim = PredictOnMovingTarget(position, target, targetVel, firingSpeed);
        GameObject cannonBall = Instantiate(cannonBallPrefab, position, Quaternion.identity);
        firingVfx.Play();
        // TODO sfx play
        cannonBall.GetComponent<Rigidbody>().velocity = BallisticVelocity(aim, firingAngle, firingSpeed);
        // cannonBall.GetComponent<Rigidbody>().velocity = aim;
        cannonBall.transform.SetParent(ScrollingPlane.Instance.transform);
        Destroy(cannonBall, 10f);
    }

    private Vector3 BallisticVelocity(Vector3 destination, float angle, float bulletSpeed)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction

        float b = (float) (0.5 * Mathf.Asin((dist * Physics.gravity.magnitude) / (bulletSpeed * bulletSpeed)));
        dir.y = dist * Mathf.Tan(b); // set dir to the elevation angle.
        dist += height / Mathf.Tan(b); // Correction for small height differences
        
        // float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        // dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        // dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        // float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        
        Debug.LogFormat("dir {0}, dir norm {1}, vel {2}, angle {3}, target {4}, res {5}", dir, dir.normalized, bulletSpeed, b, destination, dir.normalized * bulletSpeed);
        return bulletSpeed * dir.normalized; // Return a normalized vector.
    }

    private Vector3 PredictOnMovingTarget(Vector3 shooter, Vector3 target, Vector3 velTarget, float bulletSpeed)
    {
        Debug.LogFormat("predict from {0}, to {1}, at {2}", shooter, target, bulletSpeed);
        Vector3 relativePos = target - shooter;
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

        Debug.LogFormat("aim at {0}, t1 {1}, t2 {2}", (target + velTarget * (float) t), t1, t2);
        return velTarget * (float) t + target;
    }
    
}