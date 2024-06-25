using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCollider : MonoBehaviour
{
    [SerializeField] private GameObject handLeft;
    [SerializeField] private GameObject handRight;
    [SerializeField] private GameObject twoHands;

    private void Start()
    {
        if(handLeft != null)
        {
            HandLeftOff();
        }
        if(handRight != null)
        {
            HandRightOff();
        }
        if(twoHands != null)
        {
            TwoHandsOff();
        }
    }
    public void HandLeftOn()
    {
        handLeft.SetActive(true);
    }
    public void HandLeftOff()
    {
        handLeft.SetActive(false);
    }
    public void HandRightOn()
    {
        handRight.SetActive(true);
    }
    public void HandRightOff()
    {
        handRight.SetActive(false);
    }
    public void TwoHandsOff()
    {
        twoHands.SetActive(false);
    }

    public void TwoHandsOn()
    {
        twoHands.SetActive(true);
    }
}
