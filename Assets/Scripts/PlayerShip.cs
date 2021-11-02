using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : Ship
{
    public UnityAction OnFatalHit;
    
    public ObservableInt gold;
    public Cannon cannon;
    public int firingCooldown = 1;
    private float _timer;

    private bool CanShoot => !(cannon == null) && _timer > firingCooldown;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        
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
                EnemyShip enemyShip = raycastHit.collider.gameObject.GetComponent<EnemyShip>();
                if (enemyShip != null)
                {
                    if (CanShoot)
                    {
                        cannon.Fire(enemyShip.transform.position, Vector3.back * 50f);
                        _timer = 0;
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
