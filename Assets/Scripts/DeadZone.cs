using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Poolable poolable = other.gameObject.GetComponent<Poolable>();
        if (poolable)
        {
            GameObjectPoolController.Enqueue(poolable);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
