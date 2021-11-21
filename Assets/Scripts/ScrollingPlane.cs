using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ScrollingPlane : MonoSingleton<ScrollingPlane>
{
    public float steeringSpeed = 1f;
    public float scrollingSpeed = 50f;
    public GameObject isle;
    public bool Enabled { get; set; }

    private void Start()
    {
        Reset();
        Enabled = false;
    }


    public void Reset()
    {
        ResetSpawnedObjects();
        SpawnIsle();
    }

    void FixedUpdate()
    {
        if(!Enabled) return;
        
        Vector3 lateralVector = Steer();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            // transform.GetChild(i).Translate(Vector3.back * scrollingSpeed * Time.deltaTime + lateralVector * Time.deltaTime);
            transform.GetChild(i).Translate(lateralVector * Time.deltaTime);
        }
    }

    private Vector3 Steer()
    {
        var axis = CrossPlatformInputManager.GetAxis("Horizontal");
        return Vector3.left * axis * steeringSpeed;
    }


    private void ResetSpawnedObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void SpawnIsle()
    {
        var instantiate = Instantiate(isle, transform);
        Spawn(instantiate);
    }

    public void Spawn(GameObject o)
    {
        o.transform.SetParent(gameObject.transform);
        o.GetComponent<Rigidbody>().velocity += Vector3.back * scrollingSpeed;
    }
}
