using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized]
    public string currentMode;
    [System.NonSerialized]
    public bool canChangeState = true;
    [System.NonSerialized]
    public bool isInPlayingMode = true;
    [System.NonSerialized]
    public bool isAttackModeChange = false;
    [System.NonSerialized]
    public bool isClickToChangeAttackMode = false;
    [System.NonSerialized]
    public bool isInPauseMode = false;
    [System.NonSerialized]
    public GameData gameData;
    private void OnEnable()
    {
        gameData = new GameData();
    }


    public void SwitchMode(string s)
    {
        currentMode = s;
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Return))
        //{
        //    Debug.Log(currentMode);
        //}
    }
}
