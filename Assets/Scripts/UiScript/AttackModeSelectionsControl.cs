using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModeSelectionsControl : MonoBehaviour
{
    [SerializeField] private GameObject switchIcon;
    [SerializeField] private List<GameObject> list;
    private int currentSelection;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentSelection = 0;
        for(int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<AttackModeSelections>().Off();
            if (i == 0)
            {
                list[i].GetComponent<AttackModeSelections>().On();
                GameManager.Instance.SwitchMode(list[i].gameObject.name);
            }
            list[i].GetComponent<AttackModeSelections>().selectionChange += SetSelection;
            list[i].GetComponent<AttackModeSelections>().tab += Tab;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<AttackModeSelections>().selectionChange -= SetSelection;
            list[i].GetComponent<AttackModeSelections>().tab -= Tab;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !GameManager.Instance.isInPauseMode)
        {
            Tab();
        }
        if (switchIcon.activeInHierarchy.Equals(false) && Input.GetKeyDown(KeyCode.Return) && !GameManager.Instance.isInPauseMode)
        {
            list[currentSelection].GetComponent<AttackModeSelections>().Confirm();
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !GameManager.Instance.isInPauseMode)
        {
            currentSelection++;
            if (currentSelection == list.Count) currentSelection = 0;
            SetSelection(list[currentSelection]);
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !GameManager.Instance.isInPauseMode)
        {
            currentSelection--;
            if (currentSelection < 0) currentSelection = list.Count - 1;
            SetSelection(list[currentSelection]);
        }
    }
    public void SetSelection(GameObject gameObject)
    {
        int index = list.IndexOf(gameObject);
        currentSelection = index;
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
            {
                list[i].GetComponent<AttackModeSelections>().On();
            }
            else
            {
                list[i].GetComponent<AttackModeSelections>().Off();
            }
        }
    }
    public void Tab()
    {
        anim.SetTrigger("Show");
        AudioManager.Instance.asUi.Play();
    }
    public void SetCurrentMode()
    {
        if (!list[currentSelection].gameObject.name.Equals(GameManager.Instance.currentMode))
        {
            //Debug.Log(list[currentSelection].gameObject.name);
            for (int i = 0; i < list.Count; i++)
            {
                if (i != currentSelection)
                {
                    currentSelection = i;
                    SetSelection(list[currentSelection]);
                    break;
                }
            }
        }
    }
}
