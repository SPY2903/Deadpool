using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackModeSelections : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler
{
    [SerializeField] private GameObject selectionBorder;
    public event Action<GameObject> selectionChange;
    public event Action tab;
    private Animator anim;

    private void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        On();
        selectionChange?.Invoke(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Off();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.isClickToChangeAttackMode = true;
        Confirm();
    }
    public void On()
    {
        selectionBorder.SetActive(true);
    }
    public void Off()
    {
        selectionBorder.SetActive(false);
    }
    public void Confirm()
    {
        if (!GameManager.Instance.currentMode.Equals(gameObject.name))
        {
            GameManager.Instance.isAttackModeChange = true;
            GameManager.Instance.SwitchMode(gameObject.name);
        }
        else
        {
            GameManager.Instance.isAttackModeChange = false;
        }
        tab.Invoke();
    }
}
