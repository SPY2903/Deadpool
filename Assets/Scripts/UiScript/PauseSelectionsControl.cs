using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PauseSelectionsControl : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectMaterial;
    [SerializeField] private List<GameObject> list;
    [SerializeField] private PauseResume pauseResume;
    private int currentSelection;
    private void OnEnable()
    {
        currentSelection = 0;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<TextMeshProUGUI>().fontMaterial = defaultMaterial;
            if (i == 0) list[i].GetComponent<TextMeshProUGUI>().fontMaterial = selectMaterial;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelection++;
            if (currentSelection == list.Count) currentSelection = 0;
            SetSelection(currentSelection);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSelection--;
            if (currentSelection < 0) currentSelection = list.Count - 1;
            SetSelection(currentSelection);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (currentSelection == 0)
            {
                pausePanel.SetActive(false);
                pauseResume.Resume();
                pauseResume.InPlayMode();
                pauseResume.OutPauseMode();
            }
            if(currentSelection == 1)
            {
                pausePanel.SetActive(false);
                exitPanel.SetActive(false);
                settingPanel.SetActive(true);
            }
            if(currentSelection == 2)
            {
                pausePanel.SetActive(false);
                exitPanel.SetActive(true);
            }
        }
    }
    public void SetSelection(int index)
    {
        AudioManager.Instance.asUi.Play();
        currentSelection = index;
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
            {
                list[i].GetComponent<TextMeshProUGUI>().fontMaterial = selectMaterial;
            }
            else
            {
                list[i].GetComponent<TextMeshProUGUI>().fontMaterial = defaultMaterial;
            }
        }
    }
}
