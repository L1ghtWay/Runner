using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public Transform endPoint;
    
    void Start()
    {        
        WorldController.instance.OnPlatformMovement += TryDelAndAddEnvironment;
        
    }
    
    private void TryDelAndAddEnvironment()
    {
        if (transform.position.z < WorldController.instance.minZ + 40)
        {
            WorldController.instance.worldBuilder.CreateEnvironments();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {   
        if( WorldController.instance != null)     
            WorldController.instance.OnPlatformMovement -= TryDelAndAddEnvironment;
    }
}
