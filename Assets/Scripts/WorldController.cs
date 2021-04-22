using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float speed = 10f;  
    public float minZ = -100;
    public WorldBuilder worldBuilder;

    public delegate void TruToDelAndAddPlatform();
    public event TruToDelAndAddPlatform OnPlatformMovement;

    public static WorldController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        StartCoroutine(OnPlatformMovementCoroutine());
        StartCoroutine(SpeedIncrease());
        PlayerController.instance.PlayerIsNotAlive += StopPlatformMovement;
    }

    void Update()
    {
        transform.position -= Vector3.forward * speed * Time.deltaTime;        
    }  

    private void StopPlatformMovement()
    {
        speed = 0;
    }

    IEnumerator OnPlatformMovementCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            OnPlatformMovement?.Invoke();
        }        
    }    
    IEnumerator SpeedIncrease ()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            speed += 2f;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
