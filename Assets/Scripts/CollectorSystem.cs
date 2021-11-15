using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectorSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if(other.gameObject.GetComponent<Collectable>())
        {
            Collectable collectable = obj.GetComponent<Collectable>();
            collectable.Collect();
        }
    }
}