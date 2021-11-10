using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.GetHit(damage);
        }
        // TODO vfx sfx
        Destroy(gameObject);
    }
}
