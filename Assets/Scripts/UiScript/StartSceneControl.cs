using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartSceneControl : MonoBehaviour
{
    [SerializeField] private GameObject pressStartPanel;
    [SerializeField] private GameObject newGamePanel;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private TextMeshProUGUI txtTittle;
    [SerializeField] private PlayerData data;
    [SerializeField] private CharacterDetail deadpoolDetail;
    private Animator tittle;
    private Animator newGamePanelAnim;
    private Animator continuePanelAnim;
    // Start is called before the first frame update
    void Start()
    {
        tittle = txtTittle.GetComponent<Animator>();
        newGamePanelAnim = newGamePanel.GetComponent<Animator>();
        continuePanelAnim = continuePanel.GetComponent<Animator>();
        GameData gd = GameManager.Instance.gameData.Load();
        data.exp = gd.exp;
        data.levelPass = gd.levelPass;
        data.currentDamePoint = gd.currentDamePoint;
        data.currentHealthPoint = gd.currentHealPoint;
        data.currentRecoveryPoint = gd.currentRecoveryPoint;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && pressStartPanel.activeInHierarchy){
            pressStartPanel.SetActive(false);
            tittle.SetTrigger("Start");
            SoundUi();
            if(data.levelPass == 0)
            {
                newGamePanel.SetActive(true);
                newGamePanelAnim.SetTrigger("Start");
            }
            else
            {
                continuePanel.SetActive(true);
                continuePanelAnim.SetTrigger("Start");
            }
        }
    }
    public void SoundUi()
    {
        AudioManager.Instance.asUi.Play();
    }
}
