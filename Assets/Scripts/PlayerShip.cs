using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : MonoBehaviour
{
    public UnityAction OnFatalHit;
    
    public ObservableInt Gold;
    public ObservableInt Distance;
    public int Lives = 1;
    public Cannon Cannon;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
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
                    StartCoroutine(Cannon.Fire(enemy));
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
        else if (obj.GetComponent<Enemy>())
        {
            GetHit();
        }
        else if(other.gameObject.GetComponent<Collectable>())
        {
            Collectable collectable = obj.GetComponent<Collectable>();
            Gold.Value += collectable.Worth;
            Destroy(obj);
            Debug.LogFormat("Collected {0} gold. {1} total", collectable.Worth, Gold);
        }
    }

    public void GetHit()
    {
        Lives--;
        if (Lives <= 0)
        {
            // TODO sunk animation
            OnFatalHit?.Invoke();
            Destroy(gameObject);
        }
    }

}
