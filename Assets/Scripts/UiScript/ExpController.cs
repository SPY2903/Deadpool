using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExpController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private CharacterDetail characterDetail;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI expTxt;
    [Header("Dame")]
    [SerializeField] private List<Image> dameStep;
    [SerializeField] private GameObject panelDameConfirm;
    [Header("Health")]
    [SerializeField] private List<Image> healthStep;
    [SerializeField] private GameObject panelHealthConfirm;
    [Header("Recovery")]
    [SerializeField] private List<Image> recoveryStep;
    [SerializeField] private GameObject panelRecoveryConfirm;
    [Header("Notification")]
    [SerializeField] private GameObject panelNotEnoughExp;
    [Header("Effect")]
    [SerializeField] ParticleSystem effect;
    private bool isUpgradeHealth;
    private bool isUpgrade;

    private void OnEnable()
    {
        UpdateTxtExp();
        UpdatePower();
    }

    public void UpdateTxtExp()
    {
        expTxt.text = "EXP:";
        char[] arr = playerData.exp.ToString().ToCharArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].Equals('1'))
            {
                expTxt.text += "I";
            }
            else if (arr[i].Equals('0'))
            {
                expTxt.text += "O";
            }
            else
            {
                expTxt.text += arr[i];
            }
        }
    }
    public void UpdatePower()
    {
        for(int i = 0; i < playerData.currentDamePoint; i++)
        {
            dameStep[i].fillAmount = 1;
        }
        for(int i = 0; i < playerData.currentHealthPoint; i++)
        {
            healthStep[i].fillAmount = 1;
        }
        for(int i = 0; i < playerData.currentRecoveryPoint; i++)
        {
            recoveryStep[i].fillAmount = 1;
        }
    }
    public void IncreaseDamePoint()
    {
        if(playerData.exp != 0)
        {
            playerData.exp -= 20;
            playerData.currentDamePoint += playerData.currentDamePoint == 5 ? 0 : 1;
            characterDetail.dame += playerData.currentDamePoint == 5 ? 0 : 20;
            UpdateTxtExp();
            UpdatePower();
            isUpgrade = true;
        }
        else
        {
            panelNotEnoughExp.SetActive(true);
            isUpgrade = false;
        }
    }
    public void IncreaseHealthPoint()
    {
        if(playerData.exp != 0)
        {
            playerData.exp -= 20;
            playerData.currentHealthPoint += playerData.currentHealthPoint == 5 ? 0 : 1;
            characterDetail.health += playerData.currentHealthPoint == 5 ? 0 : 20;
            isUpgradeHealth = true;
            isUpgrade = true;
            UpdateTxtExp();
            UpdatePower();
        }
        else
        {
            panelNotEnoughExp.SetActive(true);
            isUpgrade = false;
        }
    }
    public void IncreaseRecoveryPoint()
    {
        if(playerData.exp != 0)
        {
            playerData.exp -= 20;
            playerData.currentRecoveryPoint += playerData.currentRecoveryPoint == 5 ? 0 : 1;
            characterDetail.healthRecover += playerData.currentHealthPoint == 5 ? 0 : 5;
            isUpgrade = true;
            UpdateTxtExp();
            UpdatePower();
        }
        else
        {
            panelNotEnoughExp.SetActive(true);
            isUpgrade = false;
        }
    }
    public void CheckDisPlayDamePanel()
    {
        if(playerData.currentDamePoint != 5)
        {
            panelDameConfirm.SetActive(true);
        }
    }
    public void CheckDisplayHealthPanel()
    {
        if(playerData.currentHealthPoint != 5)
        {
            panelHealthConfirm.SetActive(true);
        }
    }
    public void CheckDisplayRecoveryPanel()
    {
        if(playerData.currentRecoveryPoint != 5)
        {
            panelRecoveryConfirm.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (isUpgrade)
        {
            effect?.Stop();
            effect?.Play();
        }
            if (isUpgradeHealth)
        {
            playerController.UpgradeHealth();
            isUpgradeHealth = false;
        }
    }

}
