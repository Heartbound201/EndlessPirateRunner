using UnityEngine;

public class EnemyShip : Enemy
{
    public int lives;
    
    public CannonSystem cannonSystem;
    public DropSystem dropSystem;

    private void Update()
    {
        // don't shoot if behind the player
        if (!cannonSystem || transform.position.z <= 100 || transform.position.z >= 400 ||
            cannonSystem.IsObstructed(Vector3.zero)) return;
        
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