using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseSelections : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private int index;
    [SerializeField] private PauseSelectionsControl pauseSelectionsControl;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.asUi.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pauseSelectionsControl.SetSelection(index);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
