using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Prototype/Obstacle")]
public class ObstaclePrototype : EntityPrototype
{
    public int damage;
    public override GameObject Build(GameObject obj)
    {
        var component = obj.GetComponent<Obstacle>();
        component.collisionDamage = damage;
        
        // give the object a random rotation
        Transform graphics = obj.transform.Find("Graphics");
        if(graphics)
        {
            Quaternion currentRotation = graphics.rotation;
            graphics.rotation =
                Quaternion.Euler(new Vector3(currentRotation.x, Random.Range(0, 360), currentRotation.z));
        }
        return obj;
    }
}
