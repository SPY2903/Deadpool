using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class Selections : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material enterMaterial;
    private TextMeshProUGUI text;
    public event Action<GameObject> selectionChange;

    private void Awake()
    {
        if (TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI txt))
        {
            text = txt;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    toggle = !toggle;
        //    if (toggle)
        //    {
        //        On();
        //    }
        //    else
        //    {
        //        Off();
        //    }
        //}
    }
    public void On()
    {
        text.fontMaterial = selectMaterial;
    }
    public void Off()
    {
        text.fontMaterial = defaultMaterial;
    }
    public void Enter()
    {
        text.fontMaterial = enterMaterial;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Enter();
        AudioManager.Instance.asUi.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        On();
    }
    public void ExtApp()
    {
        Application.Quit();
    }
}
