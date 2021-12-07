using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public string cannonBallPoolKey;
    public GameObject cannonBallPrefab;
    [Range(10f, 45f)]
    public float firingAngle;
    public float gravity;
    public ParticleSystem firingVfx;
    public AudioClipSO firingSfx;

    private void Awake()
    {
        if (GameObjectPoolController.AddEntry(cannonBallPoolKey, cannonBallPrefab, 10, 20))
            Debug.Log("Pre-populating pool. key:" + cannonBallPoolKey);
        else
            Debug.Log(cannonBallPoolKey + "Pool already configured");
    }

    public void Fire(Vector3 target)
    {
        Vector3 position = transform.position;
        Poolable obj = GameObjectPoolController.Dequeue(cannonBallPoolKey);
        obj.transform.position = position;
        obj.gameObject.SetActive(true);
        obj.transform.localScale = Vector3.one;
        
        firingVfx.Play();
        AudioManager.Instance.PlaySFX(firingSfx);
        var rb = obj.GetComponent<Rigidbody>();
        rb.velocity = BallisticVelocity(target, firingAngle);
        var ball = obj.GetComponent<CannonBall>();
        ball.gravity = gravity;
    }

    public Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Debug.LogFormat("BallisticVelocity: destination {0}, angle {1}", destination, angle); 
        
        Vector3 dir = destination - transform.position; // get Target Direction
        // float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        // dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * gravity / Mathf.Sin(2 * a));
        
        Debug.LogFormat("BallisticVelocity: velocity {0}, velocity * dir.normalized {1}", velocity, dir.normalized * velocity);

        return velocity * dir.normalized; // Return a normalized vector.
    }

    public float Reach()
    {
        return 250f;
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