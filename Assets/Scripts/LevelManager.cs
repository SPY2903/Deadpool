using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject loaderPanel;
    [SerializeField] private Image progressBar;
    private float target;
    
    public async void LoadSceneAsync(string sceneName)
    {
        target = 0;
        progressBar.fillAmount = 0;
        loaderPanel.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        do
        {
            await Task.Delay(100);
            target = asyncLoad.progress;
        } while (asyncLoad.progress < .9f);
        await Task.Delay(100);
        asyncLoad.allowSceneActivation = true;
        loaderPanel.SetActive(false);

    }
    void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, Time.deltaTime * 3);
    }
}
