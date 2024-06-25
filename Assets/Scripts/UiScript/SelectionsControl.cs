using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionsControl : MonoBehaviour
{
    [SerializeField] private string selectionName;
    [SerializeField] private PlayerData data;
    [SerializeField] private CharacterDetail characterDetail;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private List<GameObject> list;
    private int currentSelection;

    private void Awake()
    {
        currentSelection = 0;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Selections>().Off();
            if (i == 0) list[i].GetComponent<Selections>().On();
            list[i].GetComponent<Selections>().selectionChange += SetSelection;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Selections>().selectionChange -= SetSelection;
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelection++;
            if (currentSelection == list.Count) currentSelection = 0;
            SetSelection(list[currentSelection]);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSelection--;
            if (currentSelection < 0) currentSelection = list.Count - 1;
            SetSelection(list[currentSelection]);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            AudioManager.Instance.asUi.Play();
            if (selectionName.Equals("new game"))
            {
                if(currentSelection == 0)
                {
                    data.levelPass = 0;
                    SetDefaultParameters();
                    sceneLoader.LoadSceneAsync("Level_1");
                }
                if(currentSelection == 1)
                {
                    settingPanel.SetActive(true);
                }
                if(currentSelection == 2)
                {
                    list[list.Count - 1].GetComponent<Selections>().ExtApp();
                }
            }
            if(selectionName.Equals("continue game"))
            {
                if(currentSelection == 0)
                {
                    LoadCurrentLevel();
                }
                if(currentSelection == 1)
                {
                    SetDefaultParameters();
                    sceneLoader.LoadSceneAsync("Level_1");
                }
                if(currentSelection == 2)
                {
                    settingPanel.SetActive(true);
                }
                if (currentSelection == 3)
                {
                    list[list.Count - 1].GetComponent<Selections>().ExtApp();
                }
            }
        }
    }
    void SetSelection(GameObject gameObject)
    {
        AudioManager.Instance.asUi.Play();
        int index = list.IndexOf(gameObject);
        currentSelection = index;
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
            {
                list[i].GetComponent<Selections>().On();
            }
            else
            {
                list[i].GetComponent<Selections>().Off();
            }
        }
    }
    public void Initialize()
    {
        currentSelection = 0;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Selections>().Off();
            if (i == 0) list[i].GetComponent<Selections>().On();
            list[i].GetComponent<Selections>().selectionChange += SetSelection;
        }
    }
    public void SetDefaultParameters()
    {
        characterDetail.health = 100;
        characterDetail.healthRecover = 5;
        characterDetail.dame = 20;
        data.exp = 0;
        data.levelPass = 0;
        data.currentDamePoint = 1;
        data.currentHealthPoint = 1;
        data.currentRecoveryPoint = 1;
        GameManager.Instance.gameData.Save(new GameData(data.exp, data.levelPass, data.currentDamePoint, data.currentHealthPoint, data.currentRecoveryPoint));
    }
    public void LoadCurrentLevel()
    {
        if(data.levelPass == 2)
        {
            sceneLoader.LoadSceneAsync("Level_boss_1");
        }else if(data.levelPass == 5)
        {
            sceneLoader.LoadSceneAsync("Level_boss_2");
        }
        else
        {
            sceneLoader.LoadSceneAsync("Level_" + (data.levelPass + 1));
        }
    }
}
