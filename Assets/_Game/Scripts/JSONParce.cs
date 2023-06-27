using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Pixelplacement;

[System.Serializable]
public class SaveClassBullet
{
    public List<SaveBullet> listOfSaveBullets = new List<SaveBullet>();
}
[System.Serializable]
public class SaveBullet
{
  
    public bool isFull;
    public int bulletMergeLevel;
    public Vector3 v3BulletPos;
}




public class JSONParce : Singleton<JSONParce>
{
    public SaveClassBullet saveClassBullet;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Json") == 0)
        {
            JsonSave.Save(saveClassBullet);
            PlayerPrefs.SetInt("Json", 1);
        }
        
    }

}


public static class JsonSave
{
    public static void Save(SaveClassBullet cell)
    {
        var json = JsonUtility.ToJson(cell);
        File.WriteAllText(Application.persistentDataPath + "/cell.json", json);
    }


    public static SaveClassBullet Read()
    {
        if (File.Exists(Application.persistentDataPath + "/cell.json"))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + "/cell.json");
            return JsonUtility.FromJson<SaveClassBullet>(saveString);
        }

        return null;
    }


}
