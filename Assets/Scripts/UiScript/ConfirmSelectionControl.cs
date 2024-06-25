using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmSelectionControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> list;
    [SerializeField] private Material dMaterial;
    [SerializeField] private Material selectMaterial;
    private int currentSelection;
    private void OnEnable()
    {
        currentSelection = 0;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<TextMeshProUGUI>().fontMaterial = dMaterial;
            list[i].GetComponent<ConfirmSelections>().selection += SeletionChange;
            if (i == 0) list[i].GetComponent<TextMeshProUGUI>().fontMaterial = selectMaterial;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SeletionChange(GameObject g)
    {
        int index = list.IndexOf(g);
        currentSelection = index;
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
            {
                list[i].GetComponent<TextMeshProUGUI>().fontMaterial = selectMaterial;
            }
            else
            {
                list[i].GetComponent<TextMeshProUGUI>().fontMaterial = dMaterial;
            }
        }
    }
}
