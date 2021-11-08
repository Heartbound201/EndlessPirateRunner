using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : Entity, IDamageable
{
    public UnityAction OnFatalHit;
    
    public ObservableInt lives;
    public ObservableInt gold;
    
    private CannonSystem _cannonSystem;

    private void Awake()
    {
        _cannonSystem = GetComponent<CannonSystem>();
    }

    private void Update()
    {
        if(!_cannonSystem) return;
        var axisHorCannon = CrossPlatformInputManager.GetAxis("HorizontalCannon");
        var axisVerCannon = CrossPlatformInputManager.GetAxis("VerticalCannon");
        if (CrossPlatformInputManager.GetButton("Cannon"))
        {
            _cannonSystem.AimAt(new Vector3(axisHorCannon, 0 , axisVerCannon));
            
        }
        else
        {
            _cannonSystem.Fire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Obstacle>())
        {
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            obstacle.Collide();
        }
        else if(other.gameObject.GetComponent<Collectable>())
        {
            Collectable collectable = obj.GetComponent<Collectable>();
            collectable.Collect();
        }
    }

    public void GetHit(int damage)
    {
        lives.Value -= damage;
        if (lives.Value <= 0)
        {
            // TODO sunk animation
            OnFatalHit?.Invoke();
            Destroy(gameObject);
        }
    }

}
