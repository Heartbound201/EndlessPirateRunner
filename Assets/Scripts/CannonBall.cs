using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CannonBall : MonoBehaviour
{
    public int damage = 1;
    public float gravity;
    public string explosionPoolKey;
    public GameObject explosion;
    public AudioClipSO hitAudio;
    // public AudioClipSO explosionSfx;
    public string waterSplashPoolKey;
    public GameObject waterSplash;
    public AudioClipSO missAudio;
    // public AudioClipSO waterSplashSfx;

    private Rigidbody _rb;

    private void Awake()
    {
        if (GameObjectPoolController.AddEntry(explosionPoolKey, explosion, 10, 20))
            Debug.Log("Pre-populating pool. key:" + explosionPoolKey);
        else
            Debug.Log(explosionPoolKey + "Pool already configured");
        
        if (GameObjectPoolController.AddEntry(waterSplashPoolKey, waterSplash, 10, 20))
            Debug.Log("Pre-populating pool. key:" + waterSplashPoolKey);
        else
            Debug.Log(waterSplashPoolKey + "Pool already configured");
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Poolable obj = GameObjectPoolController.Dequeue(explosionPoolKey);
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(true);
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.GetHit(damage);
            AudioManager.Instance.PlaySFX(hitAudio);
        }
        GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(0, -1.0f, 0) * _rb.mass * gravity); 
    }

    private void Update()
    {
        if (transform.position.y <= 0)
        {
            Poolable obj = GameObjectPoolController.Dequeue(waterSplashPoolKey);
            obj.transform.position = transform.position;
            obj.gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX(missAudio);
            GameObjectPoolController.Enqueue(gameObject.GetComponent<Poolable>());
        }
    }

}
