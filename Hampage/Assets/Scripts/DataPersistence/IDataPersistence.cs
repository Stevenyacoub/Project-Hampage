using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{
    //without pass by reference because we only want to read data 
    void LoadData(GameData data);

    // pass by reference so implementing script can modify data 
    void SaveData(ref GameData data);
}
