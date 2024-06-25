using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfirmSelections : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler
{
    private TextMeshProUGUI text;
    public event Action<GameObject> selection;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.asUi.Play();
        selection?.Invoke(this.gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.asUi.Play();
    }
    private void Awake()
    {
        if (TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI txt))
        {
            text = txt;
        }
    }
}
