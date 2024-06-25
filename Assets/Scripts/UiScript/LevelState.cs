using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelState : MonoBehaviour
{
    [SerializeField] private CharacterDetail playerDetail;
    [SerializeField] private LevelDetail levelDetail;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject portal;
    [SerializeField] private TextMeshProUGUI expTxt;
    [SerializeField] private List<GameObject> expTriggers;
    //[SerializeField] private GameObject exp;
    private PortalTrigger portalTrigger;
    private void Awake()
    {
        if(expTriggers.Count != 0)
        {
            for (int i = 0; i < levelDetail.expTriggers.Count; i++)
            {
                if (levelDetail.expTriggers[i])
                {
                    expTriggers[i].SetActive(false);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        portalTrigger = portal.GetComponent<PortalTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDetail.currentHealth != 0)
        {
            if (portalTrigger.isPlayerTrigger)
            {
                if(expTriggers.Count != 0)
                {
                    int count = 0;
                    for (int i = 0; i < levelDetail.expTriggers.Count; i++)
                    {
                        if (levelDetail.expTriggers[i])
                        {
                            count += 2;
                        }
                    }
                    if (count != 0)
                    {
                        expTxt.text = count + "O" + "/6O EXP";
                    }
                    else
                    {

                        expTxt.text = "O/6O EXP";
                    }
                }
                winPanel.SetActive(true);
            }
        }
        else
        {
            losePanel.SetActive(true);
        }
    }
    public void UpdateLevelPass()
    {
        playerData.levelPass += 1;
        if (playerData.levelPass > 5) playerData.levelPass = 5;
        GameManager.Instance.gameData.Save(new GameData(playerData.exp, playerData.levelPass, playerData.currentDamePoint, playerData.currentHealthPoint, playerData.currentRecoveryPoint));
    }
    public void SoundUi()
    {
        AudioManager.Instance.asUi.Play();
    }
}
