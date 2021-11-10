using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : Entity, IDamageable
{
    public UnityAction OnFatalHit;
    
    public ObservableInt lives;
    public CannonSystem cannonSystem;

    private void Awake()
    {
        cannonSystem = GetComponent<CannonSystem>();
    }

    private void Update()
    {
        if(!cannonSystem) return;
        var axisHorCannon = CrossPlatformInputManager.GetAxis("HorizontalCannon");
        var axisVerCannon = CrossPlatformInputManager.GetAxis("VerticalCannon");
        if (CrossPlatformInputManager.GetButton("Cannon"))
        {
            cannonSystem.AimAt(new Vector3(axisHorCannon, 0 , axisVerCannon));
            
        }
        else
        {
            cannonSystem.Fire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Obstacle>())
        {
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            obstacle.Collide(this);
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
