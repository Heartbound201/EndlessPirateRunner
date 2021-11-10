using UnityEngine;

public class EnemyShip : Enemy
{
    public int lives;
    
    public CannonSystem cannonSystem;
    public DropSystem dropSystem;

    private void Awake()
    {
        cannonSystem = GetComponent<CannonSystem>();
        dropSystem = GetComponent<DropSystem>();
    }

    private void Update()
    {
        if(!cannonSystem || cannonSystem.IsObstructed(Vector3.zero)) return;
        // don't shoot if behind the player
        if(transform.position.z <= 50) return;
        
        cannonSystem.Fire(Vector3.zero);
    }

    public override void GetHit(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            // TODO sunk animation
            dropSystem.DropReward();
            Destroy(gameObject);
            
        }
    }

    

    

}