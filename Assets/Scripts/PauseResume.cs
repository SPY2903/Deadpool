using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseResume : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void InPlayMode()
    {
        StartCoroutine(CountDown());
    }
    public void OutPlayMode()
    {
        GameManager.Instance.isInPlayingMode = false;
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(.25f);
        GameManager.Instance.isInPlayingMode = true;
    }
    public void InPauseMode()
    {
        GameManager.Instance.isInPauseMode = true;
    }
    public void OutPauseMode()
    {
        GameManager.Instance.isInPauseMode = false;
    }
}
