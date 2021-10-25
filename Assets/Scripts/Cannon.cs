using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{
    // public Animator animator;
    public GameObject cannonBallPrefab;

    public Transform firingPoint;

    // Update is called once per frame
    void Update()
    {
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
        Ray raycast = Camera.main.ScreenPointToRay(touch);
        Debug.DrawRay(raycast.origin, raycast.direction * 100);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            Debug.Log("Something Hit " + raycastHit.collider.name);
            if (raycastHit.collider != null) ;
            {
                Enemy enemy = raycastHit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // GameObject cannonBall = Instantiate(cannonBallPrefab, firingPoint.transform.position, Quaternion.identity);
                    // cannonBall.GetComponent<Rigidbody>().velocity = BallisticVelocity(enemy.transform.position - Vector3.back * 50f, 45f);
                    // Destroy(cannonBall, 10f);
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
        return velocity * dir.normalized; // Return a normalized vector.
    }
}