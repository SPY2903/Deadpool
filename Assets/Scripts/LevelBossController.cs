using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBossController : MonoBehaviour
{
    [SerializeField] private CharacterDetail bossDetail;
    [SerializeField] private GameObject door;
    private Animator doorAnim;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossDetail.currentHealth == 0)
        {
            doorAnim.SetTrigger("Open");
        }
    }
}
