using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiKeyBoard : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private PauseResume psrs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (!pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(true);
                psrs.InPauseMode();
                psrs.Pause();
                psrs.OutPlayMode();
                AudioManager.Instance.asUi.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            if (!upgradePanel.activeInHierarchy)
            {
                upgradePanel.SetActive(true);
                psrs.Pause();
                psrs.OutPlayMode();
                AudioManager.Instance.asUi.Play();
            }
        }
    }
}
