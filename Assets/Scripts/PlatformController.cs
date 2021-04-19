using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform endPoint;
    
    void Start()
    {
        WorldController.instance.OnPlatformMovement += TryDelAndAddPlatform;        
    }

    private void TryDelAndAddPlatform()
    {
        if (transform.position.z < WorldController.instance.minZ)
        {
            WorldController.instance.worldBuilder.CreatePlatform();
            Destroy(gameObject);
        }
    }    

    private void OnDestroy()
    {
        if (WorldController.instance != null)
            WorldController.instance.OnPlatformMovement -= TryDelAndAddPlatform;       
    }
}
