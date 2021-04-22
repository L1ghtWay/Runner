using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataFile", menuName = "DataFile")]
public class SaveData : ScriptableObject
{
    public int coinsBalance;
    public float recordScore;
    public float mainVolume;
    public float musicVolume;
    public float moveVolume;    
    public bool muteAllSounds;
}
