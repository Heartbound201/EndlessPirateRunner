using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyShip : Enemy
{
    public int lives;
    [FormerlySerializedAs("rewards")] public List<Drop> drops = new List<Drop>();
    public Cannon cannon;
    public int firingCooldown = 3;
    private float _timer;

    private bool CanFire
    {
        get
        {
            var position = transform.position;
            return !(cannon == null) && _timer > firingCooldown && position.z < 400 && position.z > 50 && !IsObstructed();
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(CanFire)
        {
            Fire();
            _timer = 0;
        }
    }

    private void Fire()
    {
        cannon.Fire(Vector3.zero);
    }

    public override void GetHit(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            // TODO sunk animation
            DropReward();
            Destroy(gameObject);
            
        }
    }

    private void DropReward()
    {
        if (drops.Count > 0)
        {
            // TODO get weighted random reward
            Drop drop = drops[Random.Range(0, drops.Count)];
            GameObject o = Instantiate(drop.item.prefab, transform.position, Quaternion.identity);
            o.transform.SetParent(ScrollingPlane.Instance.transform);
        }
    }

    private bool IsObstructed()
    {
        if (Physics.Linecast (cannon.transform.position, Vector3.zero, out RaycastHit hit))
        {
            Debug.DrawLine(cannon.transform.position, Vector3.zero, Color.yellow, 3f);
            Debug.Log("Did Hit " + hit.collider.name);
            if (hit.collider.GetComponent<PlayerShip>()) return false;
            return true;
        }

        return false;
    }

}