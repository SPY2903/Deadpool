using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandToSword : MonoBehaviour
{
    [Header("------Object------")]
    [SerializeField] private GameObject swordLeftBack;
    [SerializeField] private GameObject swordRightBack;
    [SerializeField] private GameObject swordLeftHand;
    [SerializeField] private GameObject swordRightHand;
    [Header("------Collider------")]
    [SerializeField] private GameObject punchCollLeft;
    [SerializeField] private GameObject punchCollRight;
    [SerializeField] private GameObject swordCollLeft;
    [SerializeField] private GameObject swordCollRight;
    private void Awake()
    {
        HandToBack();
        punchCollLeft.SetActive(false);
        punchCollRight.SetActive(false);
        swordCollLeft.SetActive(false);
        swordCollRight.SetActive(false);
    }


    public void BackToHand()
    {
        swordLeftBack.SetActive(false);
        swordRightBack.SetActive(false);
        swordLeftHand.SetActive(true);
        swordRightHand.SetActive(true);
    }
    public void HandToBack()
    {
        swordLeftHand.SetActive(false);
        swordRightHand.SetActive(false);
        swordLeftBack.SetActive(true);
        swordRightBack.SetActive(true);
    }
    public void CantChangeState()
    {
        GameManager.Instance.canChangeState = false;
    }
    public void CanChangeState()
    {
        GameManager.Instance.canChangeState = true;
    }
    public void PunchLeftStart()
    {
        punchCollLeft.SetActive(true);
    }
    public void PunchRightStart()
    {
        punchCollRight.SetActive(true);
    }
    public void PunchLeftEnd()
    {
        punchCollLeft.SetActive(false);
    }
    public void PunchRightEnd()
    {
        punchCollRight.SetActive(false);
    }
    public void SlashLeftStart()
    {
        swordCollLeft.SetActive(true);
    }
    public void SlashRightStart()
    {
        swordCollRight.SetActive(true);
    }
    public void SlashLeftEnd()
    {
        swordCollLeft.SetActive(false);
    }
    public void SlashRightEnd()
    {
        swordCollRight.SetActive(false);
    }
    public void SwordRightHandOn()
    {
        if (GameManager.Instance.currentMode.Equals("Sword Mode"))
        {
            swordRightHand.SetActive(true);
        }
    }
    public void SwordRightHandOff()
    {
        swordRightHand.SetActive(false);
    }
}
