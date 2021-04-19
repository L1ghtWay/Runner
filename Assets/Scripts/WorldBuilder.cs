using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private Transform platformContainer;

    [SerializeField] private PlatformController startPlatforms;
    [SerializeField] private PlatformController[] easyPlatforms;
    [SerializeField] private PlatformController[] middlePlatforms;
    [SerializeField] private PlatformController[] hardPlatforms;
    [SerializeField] private EnvironmentController[] environment;

    private Transform lastPlatform;
    private Transform lastEnvironmentRight;
    private Transform lastEnvironmentLeft;

    private bool flag = true;
    private int circle = 0;

    void Start()
    {
        lastPlatform = Instantiate(startPlatforms, platformContainer.position, Quaternion.identity, platformContainer).endPoint.transform;
        lastEnvironmentRight = Instantiate(environment[0], platformContainer.position + new Vector3(28, 0, -22), Quaternion.identity, platformContainer).endPoint.transform;
        lastEnvironmentLeft = Instantiate(environment[0], platformContainer.position + new Vector3(-28, 0, -20), Quaternion.identity, platformContainer).endPoint.transform;

        for (int i = 0; i < 30; i++)
        {
            CreateEnvironments();            
        }

        CreateEasyPlatform();

        CreateEasyPlatform();

        CreateMiddlePlatform();
    }

    public void CreatePlatform()
    {
        if (circle == 0)
        {
            CreateMiddlePlatform(); // Начало нулевого круга
            return;
        }        
        
        if (circle == 1)
        {
            int lvlOne = Random.Range(0, 3);

            if (lvlOne == 0)
            {
                CreateEasyPlatform(); // 1 круг - 33% Easy
                circle = 0;
                return;
            }
            else
            {
                CreateMiddlePlatform();
                return;
            }
        }
        
        if (circle == 2)
        {
            int lvlOne = Random.Range(0, 3);

            if (lvlOne > 0)
            {
                CreateEasyPlatform(); // 2 круг - 66% Easy
                circle = 0;
                return;
            }
            else
            {
                CreateHardPlatform();
                return;
            }
        }

        if (circle == 3)
        {
            CreateEasyPlatform(); // 3 круг - Easy 100% после Hard
            circle = 0;
        }
        
    }
    public void CreateEnvironments()
    {
        if (flag)
        {
            CreateRightEnvironment();
            flag = false;
        }
        else
        {
            CreateLeftEnvironment();
            flag = true;
        }
    }

    private void CreateEasyPlatform()
    {
        Vector3 pos = lastPlatform.position + new Vector3(0,0,3);
        int index = Random.Range(0, easyPlatforms.Length);
        Transform res = Instantiate(easyPlatforms[index], pos, Quaternion.identity, platformContainer).endPoint.transform;
        lastPlatform = res;        
    }

    private void CreateMiddlePlatform()
    {
        Vector3 pos = lastPlatform.position + new Vector3(0, 0, 3);
        int index = Random.Range(0, middlePlatforms.Length);
        Transform res = Instantiate(middlePlatforms[index], pos, Quaternion.identity, platformContainer).endPoint.transform;
        lastPlatform = res;
        circle++;
    }

    private void CreateHardPlatform()
    {
        Vector3 pos = lastPlatform.position + new Vector3(0, 0, 3);
        int index = Random.Range(0, hardPlatforms.Length);
        Transform res = Instantiate(hardPlatforms[index], pos, Quaternion.identity, platformContainer).endPoint.transform;
        lastPlatform = res;
        circle++;
    }
    public void CreateRightEnvironment()
    {        
        Vector3 posRight = lastEnvironmentRight.position + new Vector3(0, 0, 3);
        int index = Random.Range(0, environment.Length);
        environment[index].transform.localScale = new Vector3(-1, 1, 1);
        Transform resRight = Instantiate(environment[index], posRight, Quaternion.identity, platformContainer).endPoint.transform;
        lastEnvironmentRight = resRight;
    }

    public void CreateLeftEnvironment()
    {        
        Vector3 posLeft = lastEnvironmentLeft.position + new Vector3(0, 0, 3);
        int index = Random.Range(0, environment.Length);
        environment[index].transform.localScale = new Vector3(1, 1, 1);
        Transform resLeft = Instantiate(environment[index], posLeft, Quaternion.identity, platformContainer).endPoint.transform;        
        lastEnvironmentLeft = resLeft;
    }

}
