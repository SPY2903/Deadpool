using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData
{
    private string savePath = "/data.game";
    public int exp = 0;
    public int levelPass = 0;
    public int currentDamePoint = 1;
    public int currentHealPoint = 1;
    public int currentRecoveryPoint = 1;
    public GameData() { }
    public GameData(int exp, int levelPass, int currentDamePoint, int currentHealPoint, int currentRecoveryPoint)
    {
        this.exp = exp;
        this.levelPass = levelPass;
        this.currentDamePoint = currentDamePoint;
        this.currentHealPoint = currentHealPoint;
        this.currentRecoveryPoint = currentRecoveryPoint;
    }

    public void Save(GameData gd)
    {
        try
        {
            string dataToSave = JsonUtility.ToJson(gd, true);
            Debug.Log(dataToSave);
            FileStream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(dataToSave);
            streamWriter.Close();
            stream.Close();
            Debug.Log(string.Concat(Application.persistentDataPath, savePath));
        }
        catch(Exception ex)
        {
            Debug.LogError("Error occured  when trying to save data from file : " + string.Concat(Application.persistentDataPath, savePath) + "\n" + ex);
        }
        
    }
    public GameData Load()
    {
        GameData loadedData;
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            try
            {
                Debug.Log(string.Concat(Application.persistentDataPath, savePath));
                string dataToLoad = "";
                FileStream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                StreamReader streamReader = new StreamReader(stream);
                dataToLoad = streamReader.ReadToEnd();
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                streamReader.Close();
                stream.Close();
                return loadedData;
                //Debug.Log(JsonUtility.ToJson(loadedData, true));
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured  when trying to load data from file : " + string.Concat(Application.persistentDataPath, savePath) + "\n" + ex);
            }
        }
        return this;
    }
    public void ResetData()
    {
        exp = 0;
        levelPass = 0;
        currentDamePoint = 1;
        currentHealPoint = 1;
        currentRecoveryPoint = 1;
    }
}
