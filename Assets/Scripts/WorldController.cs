using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : SingletonMonoBehaviour <WorldController>
{
    public float speed = 10f;  
    public float minZ = -100;
    public WorldBuilder worldBuilder;

    public delegate void TruToDelAndAddPlatform();
    public event TruToDelAndAddPlatform OnPlatformMovement;

    new void Awake()
    {
        base.Awake();
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
}
